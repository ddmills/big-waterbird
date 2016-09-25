using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    protected NetworkManager networkManager;

    protected GameManager()
    {
    }

    void Awake()
    {
        Debug.Log("awake game manager");
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
