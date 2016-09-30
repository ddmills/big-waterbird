using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public int inventorySize;
    public GameObject inventorySlotPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool visible = false;
    private CanvasGroup canvasGroup;

    void Start()
    {
        GameManager.instance.localPlayer.GetComponent<PlayerController>().inventory = this;
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotPanel = Instantiate(inventorySlotPanel);
            slots.Add(slotPanel.GetComponent<InventorySlot>());
            slotPanel.transform.SetParent(transform);
        }

        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public bool AddItem(Loot loot)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (slots[i].SetOrAdd(loot))
            {
                return true;
            }
        }
        return false;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        visible = false;
        MouseLook.LockCursor();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        visible = true;
        MouseLook.UnlockCursor();
    }

    public void ToggleVisible()
    {
        if (visible)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
