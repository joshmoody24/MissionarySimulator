using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionEffect
{
    public string name;
    [Range(0f, 1f)]
    public float successRate = 1f;

    public ActionEffect() {
        name = this.GetType().Name;
    }

    public abstract void Execute(Person actor, Action onEffectFinish);
}

public enum EffectType
{
    None,
    ChangeTopic,
    Teach,
    Ask,
    Object,
}
