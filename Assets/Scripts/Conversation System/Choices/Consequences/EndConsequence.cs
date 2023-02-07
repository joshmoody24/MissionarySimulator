using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndConsequence : Consequence
{
    public override void Execute(Character actor, Action onEffectFinish)
    {
        Debug.Log("Conversation Ended");
        throw new System.NotImplementedException();
    }
}
