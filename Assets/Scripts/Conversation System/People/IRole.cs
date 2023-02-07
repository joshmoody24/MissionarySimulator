using System.Collections.Generic;
using UnityEngine;

public interface IRole
{
    public IEnumerable<Choice> GetPossibleActions();
    public IEnumerable<ChoiceCategory> GetPossibleCategories();

    // when a person gains knowledge, run some side effects
    public void OnLearn(float amount);
}