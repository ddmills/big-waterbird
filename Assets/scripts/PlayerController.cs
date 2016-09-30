using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : NetworkBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform cameraHandle;
    public GameObject HUD;
    public Inventory inventory;

    public float moveSpeed = 8.0f;
    public float jumpForce = 7.0f;
    public float airModifier = 0.4f;
    public float terminalVerticalVelocity = -10f;

    private float cooldown = 0f;

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
        
        cooldown -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (cooldown < 0)
            {
                cooldown = 0.2f;
                CmdFire();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpLoot();
        }

        GetComponentInChildren<Animator>().SetFloat("speed", Mathf.Abs(inputZ));

    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Bullet>().direction = bulletSpawn.rotation;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 6);
    }

    void PickUpLoot()
    {
        Transform camera = Camera.main.transform;
        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, 4))
        {
            Lootable lootable = hit.collider.GetComponent<Lootable>();
            if (lootable != null)
            {
                if (inventory.AddItem(lootable.loot))
                {
                    CmdDestroyLoot(lootable.gameObject);
                }
            }
        }
    }

    [Command]
    void CmdDestroyLoot(GameObject loot)
    {
        NetworkServer.Destroy(loot);
    }

    public override void OnStartLocalPlayer()
    {
        characterController = GetComponent<CharacterController>();
        Camera.main.transform.SetParent(cameraHandle);
        Camera.main.transform.localPosition = Vector3.zero;
        MouseLook mouseLook = Camera.main.GetComponent<MouseLook>();
        mouseLook.TargetHorizontal = transform;
        mouseLook.TargetVertical = cameraHandle.transform;
        GameManager.instance.localPlayer = gameObject;
        Instantiate(HUD);
    }
}
