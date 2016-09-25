using UnityEngine;
using System.Collections;

public class NetworkMenuController : MonoBehaviour {
    private NetworkController networkController;

    void Start()
    {
        networkController = GameManager.instance.GetComponent<NetworkController>();
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
