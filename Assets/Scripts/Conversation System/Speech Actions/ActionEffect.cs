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

    public void Execute(Person actor, Action onFinish)
    {
        switch (type)
        {
            case EffectType.ChangeTopic:
                // get topic from driver
                actor.driver.PromptTopics(requiredKnowledge, (topic) =>
                {
                    ConversationManager.manager.ChangeTopic(topic);
                    onFinish();
                });
                break;

            case EffectType.Teach:
                ConversationManager.manager.Teach(power);
                onFinish();
                break;

            case EffectType.Ask:
                float knowledge = ConversationManager.manager.Inquire(power);
                Debug.Log("Revealed intelligence: " + knowledge);
                onFinish();
                break;

            case EffectType.Object:
                Debug.Log("Objections are not implemented");
                onFinish();
                break;

            default:
                onFinish();
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
