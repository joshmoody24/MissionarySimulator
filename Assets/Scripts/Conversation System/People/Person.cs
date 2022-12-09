using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public new string name;
    public IRole role;
    public IPersonDriver driver;
    // 1f = 100 IQ (average)
    public float iq;
    [SerializeField]
    public Knowledge startingKnowledge;
    [HideInInspector]
    public Knowledge knowledge;

    public Person(float iq = 1f)
    {
        this.iq = iq;
    }
    void Awake()
    {
    }

    void Start()
    {
        if (role is Nonmember) ((Nonmember)role).interest = startingKnowledge.interest;
    }

    public void InstantiateKnowledge()
    {
        // use the prototype to make a per-person copy of knowledge;
        knowledge = Instantiate(startingKnowledge);
    }

    public float GetTopicKnowledge(Topic topic)
    {
        return knowledge.GetTopicKnowledge(topic);
    }

    public void Learn(Topic topic, float baseAmount, float limit=1f)
    {
        if (limit < 0 || limit > 1) limit = 1f;
        float learnableAmount = Mathf.Clamp(limit - knowledge.GetTopicKnowledge(topic), 0f, 1f);
        float amountLearned = Mathf.Clamp(baseAmount * iq, 0f, learnableAmount);
        float newKnowledgeAmount = knowledge.GetTopicKnowledge(topic) + amountLearned;
        knowledge.SetTopicKnowledge(topic, newKnowledgeAmount);
        Debug.Log(name + " learned " + amountLearned + " about " + topic.name);
        role.OnLearn(amountLearned);
    }
}
