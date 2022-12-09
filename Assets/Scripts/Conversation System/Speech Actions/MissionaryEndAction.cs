using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Missionary Actions/End Action")]
public class MissionaryEndAction : AbstractMissionaryAction
{
    protected override void Execute()
    {
        Debug.Log("Missionary ended conversation");
    }
}
