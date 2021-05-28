using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : AgentController
{
    public GameObject playerCam;

    private Agent agent;
    private Vector3 originalCamPosition;
    private Camera mainCam;

    private readonly KeyCode[] numberKeys = { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    private void Start()
    {
        agent = GetComponent<Agent>();
        originalCamPosition = playerCam.transform.localPosition;
        mainCam = playerCam.GetComponent<Camera>();
    }

    private void Update()
    {
        agent.Attack = Input.GetMouseButton(0);
        agent.Block = Input.GetMouseButton(1);
        agent.Forwards = Input.GetKey(KeyCode.W);
        agent.Backwards = Input.GetKey(KeyCode.S);
        agent.Left = Input.GetKey(KeyCode.A);
        agent.Right = Input.GetKey(KeyCode.D);
        agent.Run = Input.GetKey(KeyCode.LeftShift);
        agent.Jump = Input.GetKey(KeyCode.Space);
        agent.Crouch = Input.GetKey(KeyCode.LeftControl);
        agent.Dodge = Input.GetKey(KeyCode.V);
        agent.Equipping = false;
        for (int i = 0; i < numberKeys.Length; i++)
        {
            if (Input.GetKeyDown(numberKeys[i]))
            {
                agent.WeaponNumKey = i;
                agent.Equipping = true;
            }
        }
        agent.UnEquipping = Input.GetKey(KeyCode.R);
        agent.Aim = mainCam.ScreenPointToRay(Input.mousePosition);
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
