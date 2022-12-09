using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "New Action", menuName = "Nonmember Actions/Topic Change Action")]
public class NonmemberTopicChangeAction : AbstractNonmemberAction
{
    private Topic selectedTopic;
    protected override void Prepare(Person actor, Action onFinish)
    {
        IEnumerable<Topic> possibleTopics = ConversationManager.manager.GetPossibleTopics();
        selectedTopic = possibleTopics.ElementAt(UnityEngine.Random.Range(0, possibleTopics.Count()));
        Execute();
        onFinish();
    }

    public void SelectTopic(Topic t)
    {
        selectedTopic = t;
        Execute();
    }

    protected override void Execute()
    {
        Debug.Log("Nonmember changed subject to " + selectedTopic.name);
        ConversationManager.manager.ChangeTopic(selectedTopic);
    }
}
