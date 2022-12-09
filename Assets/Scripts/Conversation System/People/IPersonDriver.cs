using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPersonDriver
{
    public void PromptCategories(Action<ActionCategory> callback);
    public void PromptActions(ActionCategory selectedCategory, Action<AbstractAction> callback);
    public void PromptTopics(float requiredKnowledge, Action<Topic> callback);
}
