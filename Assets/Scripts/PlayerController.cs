using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform cameraHandle;

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 6.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 6.0f;

        transform.Translate(x, 0, z);

        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10.0f;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 3);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.cyan;

        Camera.main.transform.SetParent(cameraHandle);
        Camera.main.transform.localPosition = Vector3.zero;

        MouseLook mouseLook = Camera.main.GetComponent<MouseLook>();

        mouseLook.TargetHorizontal = transform;
        mouseLook.TargetVertical = cameraHandle.transform;
    }
}
