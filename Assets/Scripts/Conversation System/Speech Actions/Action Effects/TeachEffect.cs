using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeachEffect : ActionEffect
{
    [Range(0f, 1f)]
    public float power = 1f;
    public override void Execute(Person actor, Action onEffectFinish)
    {
        ConversationManager.manager.Teach(power);
        onEffectFinish();
    }
}
