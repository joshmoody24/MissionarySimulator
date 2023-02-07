using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDriver : IPersonDriver
{
    public ConversationUIManager ui;
    private Character person;
    public PlayerDriver(Character person, ConversationUIManager ui)
    {
        this.ui = ui;
        this.person = person;
    }
    public void InitiateConversation()
    {

    }
    public Choice SelectChoice()
    {
        throw new System.NotImplementedException();
    }
}
