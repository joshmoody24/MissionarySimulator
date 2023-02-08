using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    [SerializeField] VisualTreeAsset choiceBtnTemplate;
    [SerializeField] VisualTreeAsset conversationElement;

    UIDocument doc;

    public void OnEnable()
    {
        doc = GetComponent<UIDocument>();

        ConversationManager.manager.onMessageDelivered.AddListener(AddToHistory);
        ConversationManager.manager.onRequestPlayerAction.AddListener(PromptPlayerForChoice);

    }

    public void OnDisable()
    {
        ConversationManager.manager.onMessageDelivered.RemoveListener(AddToHistory);
        ConversationManager.manager.onRequestPlayerAction.RemoveListener(PromptPlayerForChoice);
    }

    public void AddToHistory(Message message)
    {
        var conversationHistoryController = new ConversationHistoryController();
        StartCoroutine(conversationHistoryController.RefreshHistory(doc.rootVisualElement, conversationElement));
    }

    public void PromptPlayerForChoice(List<Message> possibleMessages, Action<Choice> callback)
    {
        var choiceBtnListController = new ChoiceBtnListController();
        choiceBtnListController.PopulateChoiceBtns(doc.rootVisualElement, choiceBtnTemplate, possibleMessages, callback);
    }
}
