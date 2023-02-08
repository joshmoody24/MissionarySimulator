using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPersonDriver
{
    public void SelectChoice(Character other, Action<Choice> callback);
}
