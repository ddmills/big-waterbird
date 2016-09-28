using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
    Health health;
    public RectTransform healthBar;
    public Text healthText;
    public Inventory inventory;

    void Start()
    {
        health = GameManager.instance.localPlayer.GetComponent<Health>();
    }

    void Update()
    {
        healthBar.sizeDelta = new Vector2(health.currentHealth * 2, healthBar.sizeDelta.y);
        healthText.text = "" + health.currentHealth;

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.ToggleVisible();
        }
    }
}
