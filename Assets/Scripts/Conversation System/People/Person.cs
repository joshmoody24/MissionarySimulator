using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public new string name;
    public IRole role;
    public IPersonDriver driver;
    [SerializeField]
    public Knowledge startingKnowledge;
    [HideInInspector]
    public Knowledge knowledge;

    void Start()
    {
        if (role is Nonmember) ((Nonmember)role).interest = startingKnowledge.interest;
    }

    public void InstantiateKnowledge()
    {
        // use the prototype to make a per-person copy of knowledge;
        knowledge = Instantiate(startingKnowledge);
    }

    public float? GetTopicKnowledge(Topic topic)
    {
        return knowledge.GetTopicKnowledge(topic);
    }

    public float GetTopicKnowledgeForced(Topic topic)
    {
        return knowledge.GetTopicKnowledgeForced(topic);
    }

    public void Learn(Topic topic, float baseAmount, float limit=1f)
    {
        if (limit < 0 || limit > 1) limit = 1f;
        float learnableAmount = Mathf.Clamp(limit - knowledge.GetTopicKnowledgeForced(topic), 0f, 1f);
        float amountLearned = Mathf.Clamp(baseAmount * knowledge.iq, 0f, learnableAmount);
        float newKnowledgeAmount = knowledge.GetTopicKnowledgeForced(topic) + amountLearned;
        knowledge.SetTopicKnowledge(topic, newKnowledgeAmount);
        Debug.Log(name + " learned " + amountLearned + " about " + topic.name);
        role.OnLearn(amountLearned);
    }
}
