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
    public UnityEvent<Choice, Character> onActionFinished;

    // Debug
    [Header("Debug")]
    public Topic startingTopic;


    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(this);
        if (onTopicChanged == null) onTopicChanged = new UnityEvent<Topic>();
        if (onTurnEnded == null) onTurnEnded = new UnityEvent<Character>();
        if (onActionFinished == null) onActionFinished = new UnityEvent<Choice, Character>();
    }

    void Start()
    {
        StartConversation(startingTopic);
    }
    public void StartConversation(Topic startingTopic = null)
    {
        ChangeTopic(startingTopic);
        StartNextTurn();
    }

    public void StartNextTurn()
    {
        if (activePerson == personOne) activePerson = personTwo;
        else activePerson = personOne;
        // yield return activePerson.driver.SelectChoice();
    }


    public void InitiateAction(Choice action, Character actor)
    {
        action.Execute(actor, EvaluateAction);
    }

    public void EvaluateAction(Choice action)
    {
        onActionFinished.Invoke(action, activePerson);
        InitializeEndOfTurn();
    }

    public void InitializeEndOfTurn()
    {
        onTurnEnded.Invoke(activePerson);
        StartCoroutine(EndTurn());
    }

    public IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1);
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

    public void Teach(float power)
    {
        // people can't teach other people beyond their own knowledge
        float limit = activePerson.knowledge.GetKnowledgeOf(currentTopic);
        GetOtherPerson().Learn(currentTopic, power, limit);
    }



}