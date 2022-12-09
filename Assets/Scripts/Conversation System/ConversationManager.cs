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

    private void Awake()
    {
        if (manager == null) manager = this;
        else Destroy(this);
        if (onTopicChanged == null) onTopicChanged = new UnityEvent<Topic>();
        if (onTurnEnded == null) onTurnEnded = new UnityEvent<Person>();
    }

    public Person personOne;
    public Person personTwo;
    private Person activePerson;

    public Topic currentTopic;

    //Actions
    [HideInInspector]
    public UnityEvent<Topic> onTopicChanged;
    [HideInInspector]
    public UnityEvent<Person> onTurnEnded;

    // Debug
    [Header("Debug")]
    public Topic startingTopic;

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
        GetCategory();
    }

    public void RestartTurn()
    {
        GetCategory();
    }

    public void GetCategory()
    {
        // onPlayerTurnStart.Invoke();
        // generate list of possible speech action categories
        activePerson.driver.PromptCategories(GetAction);
        // onPlayerActionTypesGenerated.Invoke(actionCategories);
    }

    public void GetAction(ActionCategory selectedCategory)
    {
        activePerson.driver.PromptActions(selectedCategory, (action) => InitiateAction(action, activePerson));
    }

    public void InitiateAction(AbstractAction action, Person actor)
    {
        action.Execute(actor, InitializeEndOfTurn);
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

    public float Inquire(float power)
    {
        float knowledge = GetOtherPerson().knowledge.ToDict()[currentTopic];
        return knowledge;
    }

    public Person GetOtherPerson()
    {
        return personOne == activePerson ? personTwo : personOne;
    }

    public void Teach(float power)
    {
        GetOtherPerson().Learn(currentTopic, power);
    }



}