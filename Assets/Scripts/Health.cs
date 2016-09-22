using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;
    [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthbar;
    public bool destroyOnDeath;

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                RpcRespawn();
                currentHealth = maxHealth;
            }
        }
    }
    
    void OnChangeHealth(int health)
    {
        healthbar.sizeDelta = new Vector2(health * 2, healthbar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
