using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Missionary Actions/Teach Action")]
public class MissionaryTeachAction : AbstractMissionaryAction
{
    [Range(0f, 1f)]
    public float power;

    protected override void Execute()
    {
        Debug.Log("Missionary taught about " + ConversationManager.manager.currentTopic.name + " with power of " + power);
        ConversationManager.manager.Teach(power);
    }
}
