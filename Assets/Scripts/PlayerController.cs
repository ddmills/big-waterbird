using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform cameraHandle;
    public float moveSpeed = 5;
    public float jumpforce = 20;

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 speed = transform.rotation * new Vector3(x, 0, z);

        //transform.Translate(x, 0, z);

        CharacterController cc = GetComponent<CharacterController>();

        cc.SimpleMove(speed);

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
