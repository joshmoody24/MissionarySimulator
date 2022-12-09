using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Action", menuName = "Nonmember Actions/End Action")]
public class NonmemberEndAction : AbstractNonmemberAction
{
    protected override void Execute()
    {
        Debug.Log("Nonmember ended Conversation");
    }
}
