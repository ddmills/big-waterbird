#if ENABLE_UNET
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class NetworkController : MonoBehaviour {
    private NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (NetworkServer.active || NetworkClient.active)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                LeaveLocalHost();
            }
        }
    }

    public void StartLocalHost()
    {
        Debug.Log("Start local host!");
        manager.StartHost();
    }

    public void JoinLocalHost()
    {
        Debug.Log("Join local host!");
        manager.StartClient();
    }

    public void LeaveLocalHost()
    {
        Debug.Log("Leave local host!");
        manager.StopHost();
    }
}
#endif //ENABLE_UNET