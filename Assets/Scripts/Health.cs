using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;
    [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthBar;
    public bool destroyOnDeath;
    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

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
        healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
        if (!isServer)
        {
            currentHealth = health;
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPosition = Vector3.zero;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPosition;
        }
    }
}
