using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractAction : ScriptableObject
{
    public new string name;
    public ActionCategory category;
    [Range(0f,1f)]
    public float requiredKnowledge;

    public bool isUsable()
    {
        return true;
    }

    public void Initiate(Person actor, Action onActionFinish)
    {
        Prepare(actor, onActionFinish);
    }

    protected virtual void Prepare(Person actor, Action onFinish)
    {
        // most actions skip the prepare phase and jump right to execute
        Execute();
        onFinish();
    }

    protected abstract void Execute();
}
