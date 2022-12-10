using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AskEffect : ActionEffect
{
    public override void Execute(Person actor, Action onEffectFinish)
    {
        float knowledge = ConversationManager.manager.Inquire();
        Debug.Log("Revealed intelligence: " + knowledge);
        onEffectFinish();
    }
}
