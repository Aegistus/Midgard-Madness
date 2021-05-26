using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookTarget;
    public Transform cameraPivot;
    public float xSensitivity = 1f;
    public float ySensitivity = 1f;
    public float yMin = -1f;
    public float yMax = 1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GetComponentInParent<AgentHealth>().OnAgentDeath += DisableCamera;
    }

    private void DisableCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        enabled = false;
    }

    private void Update()
    {
        transform.LookAt(lookTarget);
        //transform.RotateAround(lookTarget.position, Vector3.up, Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime);
        //transform.RotateAround(lookTarget.position, -transform.right, Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime);
        cameraPivot.Rotate(Vector3.up, Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime);
        cameraPivot.Rotate(transform.right, Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime);
        cameraPivot.eulerAngles = new Vector3(cameraPivot.eulerAngles.x, cameraPivot.eulerAngles.y, 0);
        if (transform.localPosition.y >= yMax || transform.localPosition.y <= yMin)
        {
            transform.RotateAround(lookTarget.position, -transform.right, -Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime);
        }
    }
}
