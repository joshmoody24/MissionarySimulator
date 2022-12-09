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

    public ActionEffect[] actionEffects;
    public void Execute(Person actor, Action onActionFinish)
    {
        Debug.Log(actor.name + " executed action " + name);
        foreach (var effect in actionEffects)
        {
            effect.Execute(actor, onActionFinish);
        }
    }

    public bool isUsable()
    {
        return true;
    }
}
