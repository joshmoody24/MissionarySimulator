using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPersonDriver
{
    public void PromptCategories(IEnumerable<ActionCategory> possibleCategories, Action<ActionCategory> callback);
    public void PromptActions(IEnumerable<AbstractAction> possibleActions, Action<AbstractAction> callback);
    public void PromptTopics(IEnumerable<Topic> possibleTopics, Action<Topic> callback);
}
