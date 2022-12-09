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
    }

    public Person personOne;
    public Person personTwo;
    private Person activePerson;

    public Topic currentTopic;

    // Actions
    /*
    [HideInInspector]
    public UnityEvent onPlayerTurnStart;
    [HideInInspector]
    public UnityEvent<IEnumerable<ActionCategory>> onPlayerActionTypesGenerated;
    [HideInInspector]
    public UnityEvent<IEnumerable<SpeechAction>> onPlayerActionsGenerated;
    [HideInInspector]
    public UnityEvent<IEnumerable<Topic>> onSubjectChangeRequest;
    [HideInInspector]
    public UnityEvent<SpeechAction> onPlayerActionSelected;
    [HideInInspector]
    public UnityEvent onPlayerTurnEnd;
    [HideInInspector]
    public UnityEvent onNpcTurnStart;
    */

    // Debug
    [Header("Debug")]
    public List<string> conversationHistory;
    public Topic startingTopic;

    /*
    public Dictionary<ActionExecution, Action<SpeechAction>> actionExecutions = new Dictionary<ActionExecution, Action<SpeechAction>>()
    {
        { ActionExecution.Inquire, (action) => manager.Inquire(action) },
        { ActionExecution.Teach, (action) => manager.Teach(action) },
        { ActionExecution.ChangeSubject, (action) => manager.ChangeSubjectRequest(action) },
        { ActionExecution.None, (action) => {
            if(manager.activePerson == manager.player) manager.EndPlayerTurn();
        } },
    };
    */
    // Start is called before the first frame update
    void Start()
    {
        /*
        if (onPlayerTurnStart == null) onPlayerTurnStart = new UnityEvent();
        if (onPlayerActionTypesGenerated == null) onPlayerActionTypesGenerated = new UnityEvent<IEnumerable<ActionCategory>>();
        if (onPlayerActionsGenerated == null) onPlayerActionsGenerated = new UnityEvent<IEnumerable<SpeechAction>>();
        if (onPlayerActionSelected == null) onPlayerActionSelected = new UnityEvent<SpeechAction>();
        if (onSubjectChangeRequest == null) onSubjectChangeRequest = new UnityEvent<IEnumerable<Topic>>();
        if (onPlayerTurnEnd == null) onPlayerTurnEnd = new UnityEvent();
        if (onNpcTurnStart == null) onNpcTurnStart = new UnityEvent();
        */
        StartConversation(startingTopic);
    }
    public void StartConversation(Topic startingTopic = null)
    {
        currentTopic = startingTopic;
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
        IEnumerable<ActionCategory> possibleCategories = activePerson.role.GetPossibleCategories();
        activePerson.driver.PromptCategories(possibleCategories, GetAction);
        // onPlayerActionTypesGenerated.Invoke(actionCategories);
    }

    public void GetAction(ActionCategory selectedCategory)
    {
        IEnumerable<AbstractAction> possibleActions = activePerson.role.GetPossibleActions().Where(a => a.category == selectedCategory);
        activePerson.driver.PromptActions(possibleActions, (action) => InitiateAction(action, activePerson));
    }

    public void InitiateAction(AbstractAction action, Person actor)
    {
        action.Initiate(actor, EndTurn);
    }

    public void EndTurn()
    {
        // Debug failsafe
        if(conversationHistory.Count < 1000) StartNextTurn();
    }

    public void ChangeTopic(Topic topic)
    {
        currentTopic = topic;
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

    public IEnumerable<Topic> GetPossibleTopics()
    {
        return personOne.knowledge.ToDict().Keys.Union(personTwo.knowledge.ToDict().Keys).Distinct();
    }

    public void Teach(float power)
    {
        float prevKnowledge = GetOtherPerson().knowledge.GetTopicKnowledge(currentTopic);
        // TODO: Figure out algorithm for: GetOtherPerson().knowledge.SetTopicKnowledge(currentTopic, prevKnowledge + power);
    }

}