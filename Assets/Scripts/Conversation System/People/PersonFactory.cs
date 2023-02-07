using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFactory : MonoBehaviour
{
    public ConversationUIManager ui;
    [Header("Missionary")]
    public MissionaryAction[] missionaryActions;
    [Header("Nonmember")]
    public NonmemberAction[] nonmemberActions;
    public PersonSettings[] peopleToCreate;

    void Start()
    {
        foreach(PersonSettings settings in peopleToCreate)
        {
            Character p = settings.targetObject.AddComponent<Character>();
            p.name = settings.name;
            p.driver = settings.npc ? new NpcDriver(p) : new PlayerDriver(p, ui);
            p.role = settings.missionary ? new Missionary(missionaryActions) : new Nonmember(p, nonmemberActions);
            p.startingKnowledge = settings.startingKnowledge;
            if (ConversationManager.manager.personOne == null) ConversationManager.manager.personOne = p;
            else if (ConversationManager.manager.personTwo == null) ConversationManager.manager.personTwo = p;
            p.InstantiateKnowledge();
            p.knowledge.iq = (float)settings.iq / 100f;
            if(p.driver is PlayerDriver)
            {
                foreach(Knol t in p.knowledge.topicKnowledge)
                {
                    t.revealed = true;
                }
            }
        }
    }
}

[System.Serializable]
public struct PersonSettings {
    public string name;
    [Range(50,150)]
    public int iq;
    public bool missionary, npc;
    public GameObject targetObject;
    public Knowledge startingKnowledge;
}
