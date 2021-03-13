using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AgentController
{
    public GameObject playerCam;

    private Vector3 originalCamPosition;
    private Camera mainCam;

    private readonly KeyCode[] numberKeys = { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    private void Start()
    {
        originalCamPosition = playerCam.transform.localPosition;
        mainCam = playerCam.GetComponent<Camera>();
    }

    private void Update()
    {
        Attack = Input.GetMouseButton(0);
        Block = Input.GetMouseButton(1);
        Forwards = Input.GetKey(KeyCode.W);
        Backwards = Input.GetKey(KeyCode.S);
        Left = Input.GetKey(KeyCode.A);
        Right = Input.GetKey(KeyCode.D);
        Run = Input.GetKey(KeyCode.LeftShift);
        Jump = Input.GetKey(KeyCode.Space);
        Crouch = Input.GetKey(KeyCode.LeftControl);
        Equipping = false;
        for (int i = 0; i < numberKeys.Length; i++)
        {
            if (Input.GetKeyDown(numberKeys[i]))
            {
                WeaponNumKey = i;
                Equipping = true;
            }
        }
        UnEquipping = Input.GetKey(KeyCode.R);
        Aim = mainCam.ScreenPointToRay(Input.mousePosition);
    }

    public void ShiftCameraPosition(Vector3 newPosition)
    {
        playerCam.transform.localPosition = newPosition;
    }

    public void ResetCameraPosition()
    {
        playerCam.transform.localPosition = originalCamPosition;
    }
}
