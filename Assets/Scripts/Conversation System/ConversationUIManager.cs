using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class ConversationUIManager : MonoBehaviour
{
    [Header("Action Types")]
    public Transform actionTypeButtonParent;
    public GameObject actionTypeButton;
    [Header("Actions")]
    public Transform actionButtonParent;
    public GameObject actionButton;
    [Header("Topics")]
    public Transform topicViewParent;
    public Transform topicButtonParent;
    public GameObject topicButton;

    // e.g. red = minimal knowledge, green = much knowledge
    public Color[] knowledgeTiers;

    // Start is called before the first frame update
    void Awake()
    {
        actionTypeButtonParent.gameObject.SetActive(false);
        actionButtonParent.gameObject.SetActive(false);
        topicViewParent.gameObject.SetActive(false);
    }

    public void DisplayCategoryPrompt(IEnumerable<ActionCategory> categories, Action<ActionCategory> onSelect)
    {

        actionTypeButtonParent.gameObject.SetActive(true);
        actionButtonParent.gameObject.SetActive(false);
        topicViewParent.gameObject.SetActive(false);
        foreach (Transform child in actionTypeButtonParent)
        {
            Destroy(child.gameObject);
        }
        foreach (ActionCategory c in categories)
        {
            Button b = Instantiate(actionTypeButton, actionTypeButtonParent).GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = c.name;
            b.onClick.AddListener(() => onSelect(c));
        }
    }

    public void DisplayActionPrompt(IEnumerable<AbstractAction> actions, Action<AbstractAction> onSelect)
    {
        actionTypeButtonParent.gameObject.SetActive(false);
        actionButtonParent.gameObject.SetActive(true);
        topicViewParent.gameObject.SetActive(false);
        // clear out the previous buttons
        foreach (Transform child in actionButtonParent)
        {
            Destroy(child.gameObject);
        }

        // add back button
        Button backBtn = Instantiate(actionButton, actionButtonParent).GetComponent<Button>();
        backBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Go Back";
        backBtn.onClick.AddListener(ConversationManager.manager.RestartTurn);

        // add other buttons
        foreach (AbstractAction a in actions)
        {
            Button b = Instantiate(actionButton, actionButtonParent).GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = a.name;
            b.onClick.AddListener(() => onSelect(a));
        }
    }

    public void DisplayTopicPrompt(IEnumerable<Topic> topics, Action<Topic> onSelect)
    {
        actionTypeButtonParent.gameObject.SetActive(false);
        actionButtonParent.gameObject.SetActive(false);
        topicViewParent.gameObject.SetActive(true);
        // clear out the previous buttons
        foreach (Transform child in topicButtonParent)
        {
            Destroy(child.gameObject);
        }

        // add back button
        Button backBtn = Instantiate(actionButton, topicButtonParent).GetComponent<Button>();
        backBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Go Back";
        backBtn.onClick.AddListener(ConversationManager.manager.RestartTurn);

        // add other buttons
        foreach (Topic t in topics)
        {
            Button b = Instantiate(topicButton, topicButtonParent).GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = t.name;
            b.onClick.AddListener(() => onSelect(t));
        }
    }

    void DisablePlayerControl()
    {
        actionTypeButtonParent.gameObject.SetActive(false);
        actionButtonParent.gameObject.SetActive(false);
        topicViewParent.gameObject.SetActive(false);
    }
}
