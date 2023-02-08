using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class EmptyConsequence : Consequence
{
    public override void Execute(Character actor, Action onEffectFinish)
    {
        onEffectFinish();
    }
}
