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
    public Choice SelectChoice()
    {
        throw new System.NotImplementedException();
    }
}
