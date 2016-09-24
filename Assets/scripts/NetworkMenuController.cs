using UnityEngine;
using System.Collections;

public class NetworkMenuController : MonoBehaviour {
    private NetworkController networkController;

    void Awake()
    {
        networkController = GameObject.Find("NetworkManager").GetComponent<NetworkController>();
    }
    
    public void OnClickHost()
    {
        networkController.StartLocalHost();
    }

    public void OnClickJoin()
    {
        networkController.JoinLocalHost();
    }
}
