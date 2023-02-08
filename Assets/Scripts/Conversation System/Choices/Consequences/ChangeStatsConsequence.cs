using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeStatsConsequence : Consequence
{
    public List<StatChange> statChanges;
    public override void Execute(Character actor, Action onEffectFinish)
    {
        throw new System.NotImplementedException();
    }
}
