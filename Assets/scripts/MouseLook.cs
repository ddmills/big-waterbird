using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    private Transform targetHorizontal;
    private Transform targetVertical;
    private Vector2 targetHorizontalDirection;
    private Vector2 targetVerticalDirection;

    public Transform TargetHorizontal
    {
        get
        {
            return targetHorizontal;
        }

        set
        {
            targetHorizontal = value;
            targetHorizontalDirection = targetHorizontal.localRotation.eulerAngles;
        }
    }

    public Transform TargetVertical
    {
        get
        {
            return targetVertical;
        }

        set
        {
            targetVertical = value;
            targetVerticalDirection = targetVertical.localRotation.eulerAngles;
        }
    }

    public float sensitivityHorizontal = 15F;
    public float sensitivityVertical = 15F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public float lerpFactor = 0.1f;

    float rotationHorizontal = 0F;
    float rotationVertical = 0F;

    void Start()
    {
        LockCursor();
    }

    void OnDestroy()
    {
        UnlockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }

        if (Cursor.lockState == CursorLockMode.None)
        {
            return;
        }

        rotationHorizontal += Input.GetAxis("Mouse X") * sensitivityHorizontal;
        rotationVertical += Input.GetAxis("Mouse Y") * sensitivityVertical;
        rotationVertical = ClampAngle(rotationVertical, minimumY, maximumY);

        if (targetHorizontal)
        {
            Quaternion rotation = Quaternion.AngleAxis(rotationHorizontal, targetHorizontal.up) * Quaternion.Euler(targetHorizontalDirection);
            targetHorizontal.localRotation = Quaternion.Lerp(targetHorizontal.localRotation, rotation, lerpFactor);
        }

        if (targetVertical)
        {
            Quaternion rotation = Quaternion.AngleAxis(rotationVertical, Vector3.right * -1) * Quaternion.Euler(targetVerticalDirection);
            targetVertical.localRotation = Quaternion.Lerp(targetVertical.localRotation, rotation, lerpFactor);
        }
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) {
            angle += 360F;
        }

        if (angle > 360F) {
            angle -= 360F;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
