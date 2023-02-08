using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string firstName;
    public string lastName;
    public IPersonDriver driver;

    public List<string> tags;
    public Dictionary<string, float> stats;
    public Dictionary<string, float> traits;

    [SerializeField]
    public Knowledge startingKnowledge;
    [HideInInspector]
    public Knowledge knowledge;

    void Start()
    {
    }

    public void InstantiateKnowledge()
    {
        // use the prototype to make a per-person copy of knowledge;
        knowledge = Instantiate(startingKnowledge);
    }

    public void Learn(Topic topic, float baseAmount, float limit=1f)
    {
        if (limit < 0 || limit > 1) limit = 1f;
        float learnableAmount = Mathf.Clamp(limit - knowledge.GetKnowledgeOf(topic), 0f, 1f);
        float amountLearned = Mathf.Clamp(baseAmount, 0f, learnableAmount);
        float newKnowledgeAmount = knowledge.GetKnowledgeOf(topic) + amountLearned;
        knowledge.SetKnowledgeOf(topic, newKnowledgeAmount);
        Debug.Log(name + " learned " + amountLearned + " about " + topic.name);
    }
}
