using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoiceBtnListController
{
    // UXML template for list entries
    VisualTreeAsset choiceBtn;

    // UI element references
    ScrollView ChoiceList;

    public ChoiceBtnListController()
    {
        ConversationManager.manager.onMessageDelivered.AddListener((Message m) => ChoiceList?.Clear());
    }

    public void PopulateChoiceBtns(VisualElement root, VisualTreeAsset choiceBtn, List<Message> possibleMessages, Action<Choice> callback)
    {
        this.choiceBtn = choiceBtn;
        ChoiceList = root.Q<ScrollView>("choice-btn-scroll");
        FillChoiceBtnList(possibleMessages, callback);
    }

    void FillChoiceBtnList(List<Message> possibleMessages, Action<Choice> callback)
    {
        ChoiceList.Clear();
        foreach(Message m in possibleMessages)
        {
            var newBtn = choiceBtn.Instantiate();
            newBtn.Q<Button>().clicked += () => callback.Invoke(m.choice);
            var newBtnLogic = new ChoiceBtnController();
            newBtn.userData = newBtnLogic;
            newBtnLogic.SetVisualElements(newBtn);
            newBtnLogic.SetData(m);
            ChoiceList.Add(newBtn);
        }
    }
}
