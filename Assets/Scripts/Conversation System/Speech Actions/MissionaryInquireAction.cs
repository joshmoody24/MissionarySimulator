using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Missionary Actions/Inquire Action")]
public class MissionaryInquireAction : AbstractMissionaryAction
{
    [Range(0f,1f)]
    public float power;

    protected override void Execute()
    {
        Debug.Log("Missionary inquired with power of " + power);
        Debug.Log("Nonmember knows " + ConversationManager.manager.Inquire(power) + " about " + ConversationManager.manager.currentTopic.name);
    }
}
