using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionEffect
{
    [Range(0f, 1f)]
    public float power;
    [Range(0f, 1f)]
    public float requiredKnowledge;
    [Range(0f, 1f)]
    public float successRate;
    public EffectType type;

    public void Execute(Person actor, Action onEffectFinish)
    {
        switch (type)
        {
            case EffectType.ChangeTopic:
                // get topic from driver
                actor.driver.PromptTopics(requiredKnowledge, (topic) =>
                {
                    ConversationManager.manager.ChangeTopic(topic);
                    onEffectFinish();
                });
                break;

            case EffectType.Teach:
                ConversationManager.manager.Teach(power);
                onEffectFinish();
                break;

            case EffectType.Ask:
                float knowledge = ConversationManager.manager.Inquire(power);
                Debug.Log("Revealed intelligence: " + knowledge);
                onEffectFinish();
                break;

            case EffectType.Object:
                Debug.Log("Objections are not implemented");
                onEffectFinish();
                break;

            default:
                onEffectFinish();
                break;
        }
    }
}

public enum EffectType
{
    None,
    ChangeTopic,
    Teach,
    Ask,
    Object,
}
