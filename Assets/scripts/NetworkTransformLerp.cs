﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkTransformLerp : NetworkBehaviour {
    [SyncVar] Vector3 realPosition = Vector3.zero;
    [SyncVar] Quaternion realRotation;
    private float updateInterval = 1f;
    public float lerpFactor = 0.1f;

    void Start (){
        if (GameManager.hosting){
            CmdSync(transform.position, transform.rotation);
        }
    }

    void Update ()
    {
        if (GameManager.hosting)
        {
            updateInterval += Time.deltaTime;

            if (updateInterval > 0.11f) // 9 times per second
            {
                updateInterval = 0;
                CmdSync(transform.position, transform.rotation);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, lerpFactor);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, lerpFactor);
        }
    }

    [Command]
    void CmdSync(Vector3 position, Quaternion rotation)
    {
        realPosition = position;
        realRotation = rotation;
    }
}
