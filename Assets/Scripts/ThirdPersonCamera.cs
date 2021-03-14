using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls the third person camera. Based on this thread: https://forum.unity.com/threads/third-person-controller-how-to-make-player-to-move-towards-the-direction-the-camera-is-facing.540671/
public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity;
    public Transform target;
    public float dstFromTarget;
    public Vector2 pitchMinMax;

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw; //fancy term for the horizontal movement of the camera
    float pitch; //fancy term for the vertical movement of the camera

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;

    }
}
