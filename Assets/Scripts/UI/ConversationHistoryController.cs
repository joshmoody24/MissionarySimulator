using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ConversationHistoryController
{
    public IEnumerator RefreshHistory(VisualElement root, VisualTreeAsset conversationEntry)
    {
        var list = root.Q<ScrollView>("conversation-list");
        list.Clear();
        foreach (Message m in ConversationManager.manager.history)
        {
            var entry = conversationEntry.Instantiate();
            entry.Q<Label>("result-text").text = m.parseResultText();
            list.Add(entry);
        }
        yield return new WaitForSeconds(0.1f);
        list.verticalScroller.value = list.verticalScroller.highValue;
    }
}
