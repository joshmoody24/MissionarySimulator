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
        // use the prototype to make a per-person copy of knowledge;
        knowledge = Instantiate(startingKnowledge);
    }

    public float GetTopicKnowledge(Topic topic)
    {
        return knowledge.GetTopicKnowledge(topic);
    }
}
