using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.EventSystems;

public class ConversationUIManager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI currentTopic;
    // e.g. red = minimal knowledge, green = much knowledge
    public Image knowledgeIndicatorOne;
    public Image knowledgeIndicatorTwo;
    public Sprite[] knowledgeTiers;
    public Sprite mysterySprite;

    [Header("Narration")]
    public TextMeshProUGUI narrationText;

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

    // Start is called before the first frame update
    void Awake()
    {
        actionTypeButtonParent.gameObject.SetActive(false);
        actionButtonParent.gameObject.SetActive(false);
        topicViewParent.gameObject.SetActive(false);
        ConversationManager.manager.onTopicChanged.AddListener(UpdateTopic);
        ConversationManager.manager.onTurnEnded.AddListener(HidePlayerControls);
        ConversationManager.manager.onTurnEnded.AddListener(UpdateKnowledgeIndicators);
        ConversationManager.manager.onActionFinished.AddListener(UpdateNarrationText);
    }

    public void ClearAllButtons()
    {
        foreach (Transform child in actionTypeButtonParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in actionButtonParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in topicButtonParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void DisplayCategoryPrompt(IEnumerable<ChoiceCategory> categories, Action<ChoiceCategory> onSelect)
    {

        actionTypeButtonParent.gameObject.SetActive(true);
        actionButtonParent.gameObject.SetActive(false);
        topicViewParent.gameObject.SetActive(false);
        ClearAllButtons();
        foreach (ChoiceCategory c in categories.OrderBy(c => c.order))
        {
            Button b = Instantiate(actionTypeButton, actionTypeButtonParent).GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = c.name;
            b.onClick.AddListener(() => onSelect(c));
        }
        var firstBtn = actionTypeButtonParent.GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(firstBtn);
    }

    public void DisplayActionPrompt(IEnumerable<Choice> actions, Action<Choice> onSelect)
    {
        actionTypeButtonParent.gameObject.SetActive(false);
        actionButtonParent.gameObject.SetActive(true);
        topicViewParent.gameObject.SetActive(false);

        ClearAllButtons();

        // add back button
        Button backBtn = Instantiate(actionButton, actionButtonParent).GetComponent<Button>();
        backBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Go Back";
        backBtn.onClick.AddListener(ConversationManager.manager.RestartTurn);
        EventSystem.current.SetSelectedGameObject(backBtn.gameObject);

        // add other buttons
        foreach (Choice a in actions.OrderBy(a => {
            if (a is MissionaryAction) return ((MissionaryAction)a).specialPointsCost;
            else return ((NonmemberAction)a).minAttention;
        }))
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

        ClearAllButtons();

        // add back button
        Button backBtn = Instantiate(actionButton, topicButtonParent).GetComponent<Button>();
        backBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Go Back";
        backBtn.onClick.AddListener(ConversationManager.manager.RestartTurn);
        EventSystem.current.SetSelectedGameObject(backBtn.gameObject);

        // add other buttons
        foreach (Topic t in topics)
        {
            Button b = Instantiate(topicButton, topicButtonParent).GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = t.name;
            b.onClick.AddListener(() => onSelect(t));
        }
    }

    public void UpdateTopic(Topic newTopic)
    {
        currentTopic.text = newTopic.name;
        UpdateKnowledgeIndicators();
    }

    public void UpdateKnowledgeIndicators(Character p = null)
    {
        float tierSize = 1f / knowledgeTiers.Length;

        float? personOneKnowledge = ConversationManager.manager.personOne.GetTopicKnowledge(ConversationManager.manager.currentTopic);
        float? personTwoKnowledge = ConversationManager.manager.personTwo.GetTopicKnowledge(ConversationManager.manager.currentTopic);
        if (personOneKnowledge == null) knowledgeIndicatorOne.sprite = mysterySprite;
        else knowledgeIndicatorOne.sprite = knowledgeTiers[Mathf.Clamp(Mathf.FloorToInt((float)personOneKnowledge / tierSize), 0, knowledgeTiers.Length-1)];
        if (personTwoKnowledge == null) knowledgeIndicatorTwo.sprite = mysterySprite;
        else knowledgeIndicatorTwo.sprite = knowledgeTiers[Mathf.Clamp(Mathf.FloorToInt((float)personTwoKnowledge / tierSize), 0, knowledgeTiers.Length-1)];
    }

    void HidePlayerControls(Character person)
    {
        actionTypeButtonParent.gameObject.SetActive(false);
        actionButtonParent.gameObject.SetActive(false);
        topicViewParent.gameObject.SetActive(false);
    }

    void UpdateNarrationText(Choice action, Character actor)
    {
        narrationText.text = actor.name + " used " + action.name + "!";
    }
}
