using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager manager;
    public GameConfig config;

    public Character personOne;
    public Character personTwo;
    private Character activePerson;

    public Topic currentTopic;

    //Actions
    [HideInInspector]
    public UnityEvent<Topic> onTopicChanged;
    [HideInInspector]
    public UnityEvent<Character> onTurnEnded;
    [HideInInspector]
    public UnityEvent<Message> onMessageDelivered;
    [HideInInspector]
    public UnityEvent<List<Message>, Action<Choice>> onRequestPlayerAction;

    public List<Message> history;

    // Debug
    [Header("Debug")]
    public Topic startingTopic;


    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(this);
        if (onTopicChanged == null) onTopicChanged = new UnityEvent<Topic>();
        if (onTurnEnded == null) onTurnEnded = new UnityEvent<Character>();
        if (onMessageDelivered == null) onMessageDelivered = new UnityEvent<Message>();
        if (onRequestPlayerAction == null) onRequestPlayerAction = new UnityEvent<List<Message>, Action<Choice>>();
    }

    void Start()
    {
        StartConversation(startingTopic);
    }

    public void StartConversation(Topic startingTopic = null)
    {
        history = new List<Message>();
        ChangeTopic(startingTopic);
        StartNextTurn();
    }

    public void StartNextTurn()
    {
        if (activePerson == personOne) activePerson = personTwo;
        else activePerson = personOne;
        activePerson.driver.SelectChoice(GetOtherPerson(), ExecuteChoice);
    }


    public void ExecuteChoice(Choice choice)
    {
        choice.Execute(activePerson, EvaluateChoice);
    }

    public void EvaluateChoice(Choice choice)
    {
        Message newMessage = new Message { choice = choice, receiver = GetOtherPerson(), speaker = activePerson, topic = currentTopic };
        history.Add(newMessage);
        onMessageDelivered.Invoke(newMessage);
        InitializeEndOfTurn();
    }

    public void InitializeEndOfTurn()
    {
        onTurnEnded.Invoke(activePerson);
        StartCoroutine(EndTurn());
    }

    public IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(config.conversationDelay);
        StartNextTurn();
    }

    public void ChangeTopic(Topic topic)
    {
        currentTopic = topic;
        onTopicChanged.Invoke(topic);
    }

    public float Inquire()
    {
        return GetOtherPerson().knowledge.GetKnowledgeOf(currentTopic);
    }

    public Character GetOtherPerson()
    {
        return personOne == activePerson ? personTwo : personOne;
    }

    public void RequestPlayerChoice(List<Choice> possibleChoices, Action<Choice> callback)
    {
        // map choices to messages for parsing
        var possibleMessages = possibleChoices.Select(c => new Message { choice = c, receiver = GetOtherPerson(), speaker = activePerson, topic = currentTopic }).ToList();
        onRequestPlayerAction.Invoke(possibleMessages, callback);
    }

    public void Teach(float power)
    {
        // people can't teach other people beyond their own knowledge
        float limit = activePerson.knowledge.GetKnowledgeOf(currentTopic);
        GetOtherPerson().Learn(currentTopic, power, limit);
    }
}