using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xSensitivity = 1f;
    public float ySensitivity = 1f;
    public Transform lookTarget;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.LookAt(lookTarget);
        transform.RotateAround(lookTarget.position, Vector3.up, Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime);
        transform.RotateAround(lookTarget.position, -transform.right, Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime);
    }
}
