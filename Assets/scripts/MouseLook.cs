using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public bool lockCursor = false;

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

    float rotationHorizontal = 0F;
    float rotationVertical = 0F;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        rotationHorizontal += Input.GetAxis("Mouse X") * sensitivityHorizontal;
        rotationVertical += Input.GetAxis("Mouse Y") * sensitivityVertical;
        rotationVertical = ClampAngle(rotationVertical, minimumY, maximumY);

        if (targetHorizontal)
        {
            targetHorizontal.localRotation = Quaternion.AngleAxis(rotationHorizontal, targetHorizontal.up);
            targetHorizontal.localRotation *= Quaternion.Euler(targetHorizontalDirection);
        }

        if (targetVertical)
        {
            targetVertical.localRotation = Quaternion.AngleAxis(rotationVertical, Vector3.right * -1);
            targetVertical.localRotation *= Quaternion.Euler(targetVerticalDirection);
        }
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
