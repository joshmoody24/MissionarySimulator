using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeTopicConsequence : Consequence
{
    [Range(0f, 1f)]
    public float requiredKnowledge;
    public override void Execute(Character actor, Action onEffectFinish)
    {
        throw new System.NotImplementedException();
        ConversationManager.manager.ChangeTopic(null);
        onEffectFinish();
    }
}
