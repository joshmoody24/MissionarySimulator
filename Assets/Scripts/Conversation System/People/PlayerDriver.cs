using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDriver : IPersonDriver
{
    private Character person;
    public PlayerDriver(Character person)
    {
        this.person = person;
    }
    public void InitiateConversation()
    {

    }

    public void SelectChoice(Character other, Action<Choice> callback)
    {
        var choices = person.GetPossibleChoices(other);
        ConversationManager.manager.RequestPlayerChoice(choices, SendChoice);
    }

    public void SendChoice(Choice choice)
    {
        ConversationManager.manager.EvaluateChoice(choice);
    }
}
