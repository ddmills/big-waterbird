#if ENABLE_UNET
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class NetworkController : MonoBehaviour {
    private NetworkManager manager;
    public bool joinOnStart;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    void Start()
    {
        if (joinOnStart)
        {
            if (NetworkServer.active)
            {
                JoinLocalHost();
            }
            else
            {
                StartLocalHost();
            }
        }
    }

    void Update()
    {
        if (!NetworkClient.active && !NetworkServer.active)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                StartLocalHost();
            }
        }
        if (NetworkServer.active && NetworkClient.active)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                LeaveLocalHost();
            }
        }
    }

    public void StartLocalHost(string address = "127.0.0.1", int port = 7777)
    {
        Debug.Log("Hosting " + address + ":" + port);
        manager.networkAddress = address;
        manager.networkPort = port;
        manager.StartHost();
        manager.networkAddress = address;
        manager.networkPort = port;
    }

    public void JoinLocalHost(string address = "127.0.0.1", int port = 7777)
    {
        Debug.Log("Joining " + address + ":" + port);
        manager.networkAddress = address;
        manager.networkPort = port;
        manager.StartClient();
    }

    public void LeaveLocalHost()
    {
        manager.StopHost();
    }
}
#endif //ENABLE_UNET