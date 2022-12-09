using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFactory : MonoBehaviour
{
    public ConversationUIManager ui;
    [Header("Missionary")]
    public AbstractMissionaryAction[] missionaryActions;
    [Header("Nonmember")]
    public AbstractNonmemberAction[] nonmemberActions;
    public PersonSettings[] peopleToCreate;

    void Start()
    {
        foreach(PersonSettings settings in peopleToCreate)
        {
            Person p = settings.targetObject.AddComponent<Person>();
            p.driver = settings.npc ? new NpcDriver() : new PlayerDriver(ui);
            p.role = settings.missionary ? new Missionary(missionaryActions) : new Nonmember(nonmemberActions);
            p.startingKnowledge = settings.startingKnowledge;
            if (ConversationManager.manager.personOne == null) ConversationManager.manager.personOne = p;
            else if (ConversationManager.manager.personTwo == null) ConversationManager.manager.personTwo = p;
        }
    }
}

[System.Serializable]
public struct PersonSettings {
    public bool missionary, npc;
    public GameObject targetObject;
    public Knowledge startingKnowledge;
}
