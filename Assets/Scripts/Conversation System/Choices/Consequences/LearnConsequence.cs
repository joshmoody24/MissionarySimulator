using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LearnConsequence : Consequence
{
    public override void Execute(Character actor, Action onEffectFinish)
    {
        float knowledge = ConversationManager.manager.Inquire();
        Debug.Log("Revealed intelligence: " + knowledge);
        onEffectFinish();
    }
}
