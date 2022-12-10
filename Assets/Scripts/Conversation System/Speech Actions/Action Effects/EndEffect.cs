using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndEffect : ActionEffect
{
    public override void Execute(Person actor, Action onEffectFinish)
    {
        Debug.Log("Conversation Ended");
        throw new System.NotImplementedException();
    }
}
