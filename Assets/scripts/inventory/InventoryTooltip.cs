using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventoryTooltip : MonoBehaviour, IPointerExitHandler
{
    private Loot item;
    private Text title;
    private CanvasGroup canvasGroup;
    public GameObject behaviorButtonPrefab;
    private List<GameObject> behaviorButtons = new List<GameObject>();

    void Start()
    {
        title = transform.FindChild("InventoryTooltipTitle").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        Deactivate();
    }

    public void Activate(Loot item)
    {
        this.item = item;
        title.text = item.title;
        AddBehaviorButtons();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void AddBehaviorButtons()
    {
        foreach (GameObject button in behaviorButtons)
        {
            Destroy(button);
        }

        foreach (LootBehavior behavior in item.behaviors)
        {
            behavior.loot = item;
            GameObject behaviorButton = Instantiate(behaviorButtonPrefab);
            behaviorButton.GetComponentInChildren<Text>().text = behavior.verb;
            behaviorButton.GetComponent<Button>().onClick.AddListener(Deactivate);
            behaviorButton.GetComponent<Button>().onClick.AddListener(behavior.Perform);
            behaviorButton.transform.SetParent(transform);
            behaviorButtons.Add(behaviorButton);
        }
    }

    public void Deactivate()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deactivate();
    }
}
