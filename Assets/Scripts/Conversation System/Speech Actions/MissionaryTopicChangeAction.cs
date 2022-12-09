using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Missionary Actions/Topic Change Action")]
public class MissionaryTopicChangeAction : AbstractMissionaryAction
{
    private Topic selectedTopic;
    protected override void Prepare(Person actor, Action onFinish)
    {
        // get topic from driver
        IEnumerable<Topic> possibleTopics = ConversationManager.manager.GetPossibleTopics();
        actor.driver.PromptTopics(possibleTopics, (topic) =>
        {
            SelectTopic(topic);
            onFinish();
        });
    }
    public void SelectTopic(Topic t)
    {
        selectedTopic = t;
        Execute();
    }
    protected override void Execute()
    {
        ConversationManager.manager.ChangeTopic(selectedTopic);
    }
}
