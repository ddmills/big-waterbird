using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public int inventorySize;
    public GameObject inventorySlotPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();

    void Start()
    {
        GameManager.instance.localPlayer.GetComponent<PlayerController>().inventory = this;
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotPanel = Instantiate(inventorySlotPanel);
            slots.Add(slotPanel.GetComponent<InventorySlot>());
            slotPanel.transform.SetParent(transform);
        }
    }

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (slots[i].SetOrAdd(item))
            {
                return true;
            }
        }
        return false;
    }
}
