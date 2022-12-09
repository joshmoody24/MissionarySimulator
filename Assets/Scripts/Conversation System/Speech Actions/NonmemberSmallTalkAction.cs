using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Action", menuName = "Nonmember Actions/Small Talk Action")]
public class NonmemberSmallTalkAction : AbstractNonmemberAction
{
    protected override void Execute()
    {
        Debug.Log("Nonmember engaged in small talk");
    }
}
