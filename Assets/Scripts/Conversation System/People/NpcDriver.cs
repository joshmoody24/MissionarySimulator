using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class NpcDriver : IPersonDriver
{

    // ask the npc to choose an action category from list of possible categories
    public void PromptCategories(IEnumerable<ActionCategory> possibleCategories, Action<ActionCategory> callback)
    {
        var selected = possibleCategories.ElementAt(UnityEngine.Random.Range(0, possibleCategories.Count()));
        callback(selected);
    }

    // ask the npc to choose an action from list of possible actions
    public void PromptActions(IEnumerable<AbstractAction> possibleActions, Action<AbstractAction> callback)
    {
        // algorithmically decide action
        var selected = possibleActions.ElementAt(UnityEngine.Random.Range(0, possibleActions.Count()));
        callback(selected);
    }

    public void PromptTopics(IEnumerable<Topic> possibleTopics, Action<Topic> callback)
    {
        var selected = possibleTopics.ElementAt(UnityEngine.Random.Range(0, possibleTopics.Count()));
        callback(selected);
    }
}
