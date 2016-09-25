using UnityEngine;
using System.Collections;

public class NetworkMenuController : MonoBehaviour {
    private NetworkController networkController;

    public string ipAddress = "127.0.0.1";
    public int port = 7777;

    void Start()
    {
        networkController = GameManager.instance.GetComponent<NetworkController>();
    }
    
    public void OnClickHost()
    {

        networkController.StartLocalHost(ipAddress, port);
    }

    public void OnClickJoin()
    {
        networkController.JoinLocalHost(ipAddress, port);
    }

    public void OnIPAddressChange(string value)
    {
        Debug.Log("IP CHANGED " + value);
        ipAddress = value;
    }

    public void OnPortChange(string value)
    {
        Debug.Log("PORT CHANGED " + value);
        port = int.Parse(value);
    }
}
