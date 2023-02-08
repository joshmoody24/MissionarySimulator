using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoiceBtnController 
{
    public Label title;
    public Label description;

    public void SetVisualElements(VisualElement element)
    {
        this.title = element.Q<Label>("choice-title");
        this.description = element.Q<Label>("choice-desc");
    }

    public void SetData(Message m)
    {
        title.text = m.parseName();
        description.text = m.parseDescription();
    }

}
