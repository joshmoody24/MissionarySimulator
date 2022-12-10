using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting.Dependencies.NCalc;

public  class AbstractAction : ScriptableObject
{
    public new string name;
    [TextArea(3,8)]
    public string description;
    public ActionCategory category;
    [Range(0f,1f)]
    public float requiredKnowledge;

    [SerializeReference]
    public ActionEffect[] effects;

    public void Execute(Person actor, Action<AbstractAction> onActionFinish)
    {
        Debug.Log(actor.name + " executed action " + name);

        // queue up the effects and have them call each other via callbacks
        Queue<ActionEffect> pendingEffects = new Queue<ActionEffect>();
        foreach (var effect in effects)
        {
            pendingEffects.Enqueue(effect);
        }

        // recursive callback monstrosity from the black lagoon
        // it keeps processing effects until there aren't any more
        // then it calls the onActionFinish callback
        void ProcessNextEffect(Action<AbstractAction> onActionFinish)
        {
            Action callback = pendingEffects.Count <= 0 ? () => onActionFinish(this) : () => ProcessNextEffect(onActionFinish);
            if (pendingEffects.Count <= 0) onActionFinish(this);
            else
            {
                var currentEffect = pendingEffects.Dequeue();
                currentEffect.Execute(actor, callback);
            }
        }

        ProcessNextEffect(onActionFinish);
    }

    public bool isUsable()
    {
        return true;
    }
}
