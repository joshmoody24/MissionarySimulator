using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeTopicEffect : ActionEffect
{
    [Range(0f, 1f)]
    public float requiredKnowledge;
    public override void Execute(Person actor, Action onEffectFinish)
    {
        actor.driver.PromptTopics(requiredKnowledge, (topic) =>
        {
            ConversationManager.manager.ChangeTopic(topic);
            onEffectFinish();
        });
    }
}
