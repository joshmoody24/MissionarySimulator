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
    void Start()
    {
        // use the prototype to make a per-person copy of knowledge;
        knowledge = Instantiate(startingKnowledge);
        if (role is Nonmember) ((Nonmember)role).interest = startingKnowledge.interest;
    }

    public float GetTopicKnowledge(Topic topic)
    {
        return knowledge.GetTopicKnowledge(topic);
    }

    public void Learn(Topic topic, float baseAmount)
    {
        float learnableAmount = 1f - knowledge.GetTopicKnowledge(topic);
        float amountLearned = Mathf.Clamp(baseAmount * iq, 0f, learnableAmount);
        knowledge.SetTopicKnowledge(topic, Mathf.Clamp(knowledge.GetTopicKnowledge(topic) + amountLearned, 0f, 1f));
        Debug.Log(name + " learned " + amountLearned + " about " + topic.name);
        role.OnLearn(amountLearned);
    }
}
