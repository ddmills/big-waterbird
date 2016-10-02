﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryTooltip : MonoBehaviour {
    private Loot item;
    private Text title;
    private CanvasGroup canvasGroup;

    void Start()
    {
        title = transform.FindChild("InventoryTooltipTitle").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        Deactivate();
    }

    public void Activate(Loot item)
    {
        this.item = item;
        Debug.Log(item.title);
        title.text = item.title;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void Deactivate()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}
