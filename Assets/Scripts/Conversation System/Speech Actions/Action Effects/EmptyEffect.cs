using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class EmptyEffect : ActionEffect
{
    public override void Execute(Person actor, Action onEffectFinish)
    {
        onEffectFinish();
    }
}
