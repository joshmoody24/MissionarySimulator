using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Consequence
{
    public string resultText;
    public abstract void Execute(Character actor, Action onEffectFinish);
}
