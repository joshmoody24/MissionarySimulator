using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class NpcDriver : IPersonDriver
{
    private Person person;
    public NpcDriver(Person person)
    {
        this.person = person;
    }

    // ask the npc to choose an action category from list of possible categories
    public void PromptCategories(Action<ChoiceCategory> callback)
    {
        IEnumerable<ChoiceCategory> possibleCategories = person.role.GetPossibleCategories();
        var selected = possibleCategories.ElementAt(UnityEngine.Random.Range(0, possibleCategories.Count()));
        callback(selected);
    }

    // ask the npc to choose an action from list of possible actions
    public void PromptActions(ChoiceCategory selectedCategory, Action<Choice> callback)
    {
        // algorithmically decide action
        IEnumerable<Choice> possibleActions = person.role.GetPossibleActions().Where(a => a.category == selectedCategory);
        var selected = possibleActions.ElementAt(UnityEngine.Random.Range(0, possibleActions.Count()));
        callback(selected);
    }

    public void PromptTopics(float requiredKnowledge, Action<Topic> callback)
    {
        IEnumerable<Topic> possibleTopics = person.knowledge.ToDict().Where((pair) => pair.Value > requiredKnowledge).Select((pair) => pair.Key);
        var selected = possibleTopics.ElementAt(UnityEngine.Random.Range(0, possibleTopics.Count()));
        callback(selected);
    }
}
