using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform cameraHandle;

    public float moveSpeed = 8.0f;
    public float jumpForce = 7.0f;
    public float airModifier = 0.4f;
    public float terminalVerticalVelocity = -10f;

    Vector3 previousDeltas = Vector3.zero;

    float verticalVelocity;

    CharacterController characterController;

    void Update() {
        if (!isLocalPlayer)
        {
            return;
        }

        float inputX = Input.GetAxis("Horizontal") * moveSpeed;
        float inputZ = Input.GetAxis("Vertical") * moveSpeed;

        float modifier = moveSpeed;


        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity = terminalVerticalVelocity/2;
            }
        }

        Vector3 deltas = new Vector3(inputX, verticalVelocity, inputZ);
        deltas = transform.rotation * deltas;
        previousDeltas = deltas;

        characterController.Move(deltas * Time.deltaTime);
        
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30.0f;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 6);
    }

    public override void OnStartLocalPlayer()
    {
        characterController = GetComponent<CharacterController>();

        GetComponent<MeshRenderer>().material.color = Color.cyan;

        Camera.main.transform.SetParent(cameraHandle);
        Camera.main.transform.localPosition = Vector3.zero;

        MouseLook mouseLook = Camera.main.GetComponent<MouseLook>();

        mouseLook.TargetHorizontal = transform;
        mouseLook.TargetVertical = cameraHandle.transform;
    }
}
