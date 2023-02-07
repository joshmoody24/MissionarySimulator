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

    public Choice SelectChoice()
    {
        throw new System.NotImplementedException();
    }
}
