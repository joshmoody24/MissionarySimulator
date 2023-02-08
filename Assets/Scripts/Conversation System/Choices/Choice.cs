using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Choice", menuName = "Choice")]
public class Choice : ScriptableObject
{
    public new string name;
    [TextArea(3,8)]
    public string description;
    [TextArea(6, 8)]
    public string resultText;
    [Range(0f, 1f)]
    public float requiredKnol;

    public List<string> fromTags;
    public List<string> toTags;

    public List<Trait> targetTraits;
    public List<StatRange> statRanges;

    public List<string> includedEnvironments;
    public List<string> excludedEnvironments;

    [SerializeReference]
    public Consequence[] consequences;

    public void Execute(Character actor, Action<Choice> onActionFinish)
    {
        Debug.Log(actor.name + " chose " + name);

        // queue up the effects and have them call each other via callbacks
        Queue<Consequence> pendingConsequences = new Queue<Consequence>();
        foreach (var effect in consequences)
        {
            pendingConsequences.Enqueue(effect);
        }

        // recursive callback monstrosity from the black lagoon
        // it keeps processing effects until there aren't any more
        // then it calls the onActionFinish callback
        void ProcessNextConsequence(Action<Choice> onActionFinish)
        {
            Action callback = pendingConsequences.Count <= 0 ? () => onActionFinish(this) : () => ProcessNextConsequence(onActionFinish);
            if (pendingConsequences.Count <= 0) onActionFinish(this);
            else
            {
                var currentEffect = pendingConsequences.Dequeue();
                currentEffect.Execute(actor, callback);
            }
        }

        ProcessNextConsequence(onActionFinish);
    }

    public virtual bool IsUsable()
    {
        return true;
    }
}
