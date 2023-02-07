using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDriver : IPersonDriver
{
    public ConversationUIManager ui;
    private Person person;
    public PlayerDriver(Person person, ConversationUIManager ui)
    {
        this.ui = ui;
        this.person = person;
    }
    public void InitiateConversation()
    {

    }

    public void PromptActions(ChoiceCategory selectedCategory, Action<Choice> callback)
    {
        IEnumerable<Choice> possibleActions = person.role.GetPossibleActions().Where(a => a.category == selectedCategory);
        ui.DisplayActionPrompt(possibleActions, callback);
    }

    public void PromptCategories(Action<ChoiceCategory> callback)
    {
        IEnumerable<ChoiceCategory> possibleCategories = person.role.GetPossibleCategories();
        ui.DisplayCategoryPrompt(possibleCategories, callback);
    }

    public void PromptTopics(float requiredKnowledge, Action<Topic> callback)
    {
        IEnumerable<Topic> possibleTopics = person.knowledge.ToDict().Where((pair) => pair.Value > requiredKnowledge).Select((pair) => pair.Key);
        ui.DisplayTopicPrompt(possibleTopics, callback);
    }
}
