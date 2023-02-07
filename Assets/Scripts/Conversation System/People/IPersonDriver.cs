using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPersonDriver
{
    public void PromptCategories(Action<ChoiceCategory> callback);
    public void PromptActions(ChoiceCategory selectedCategory, Action<Choice> callback);
    public void PromptTopics(float requiredKnowledge, Action<Topic> callback);
}
