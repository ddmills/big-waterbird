using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    protected NetworkManager networkManager;
    public GameObject localPlayer;

    protected GameManager()
    {
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        networkManager = GetComponent<NetworkManager>();

        Initialize();
    }

    void Initialize()
    {

    }
}
