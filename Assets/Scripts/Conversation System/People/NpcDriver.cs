using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class NpcDriver : IPersonDriver
{
    private Character person;
    public NpcDriver(Character person)
    {
        this.person = person;
    }

    public void SelectChoice(Character other, Action<Choice> callback)
    {
        var choices = person.GetPossibleChoices(other);
        callback.Invoke(choices.ElementAt(UnityEngine.Random.Range(0, choices.Count)));
    }
}
