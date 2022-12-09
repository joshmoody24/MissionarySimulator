using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriver : IPersonDriver
{
    public ConversationUIManager ui;
    public PlayerDriver(ConversationUIManager ui)
    {
        this.ui = ui;
    }
    public void InitiateConversation()
    {

    }

    public void PromptActions(IEnumerable<AbstractAction> possibleActions, Action<AbstractAction> callback)
    {
        ui.DisplayActionPrompt(possibleActions, callback);
    }

    public void PromptCategories(IEnumerable<ActionCategory> possibleCategories, Action<ActionCategory> callback)
    {
        ui.DisplayCategoryPrompt(possibleCategories, callback);
    }

    public void PromptTopics(IEnumerable<Topic> possibleTopics, Action<Topic> callback)
    {
        ui.DisplayTopicPrompt(possibleTopics, callback);
    }
}
