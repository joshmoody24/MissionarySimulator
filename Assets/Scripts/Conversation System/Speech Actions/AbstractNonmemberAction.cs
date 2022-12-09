using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractNonmemberAction : AbstractAction
{
    [Range(0f,1f)]
    public float requiredEngagement;
    [Range(0f, 1f)]
    public float interestCost;
}
