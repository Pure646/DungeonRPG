using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCharacterController : MonoBehaviour
{
    private SlimeCharacterBase slimeCharacterBase;
    private float Trans_Y;

    private void Awake()
    {
        slimeCharacterBase = GetComponent<SlimeCharacterBase>();
    }
    private void Start()
    {
        InputSystem.Instance.Jump += Jumping;
        InputSystem.Instance.RightShiftHoldingLocked += RightShiftLocked;
        InputSystem.Instance.LeftShiftRunning += Running;
        InputSystem.Instance.Walk += Walking;
    }

    private void Update()
    {
        TransRotation();
    }

    [SerializeField] private Transform cameraAim;
    private float rotation_X = 0f;
    private float rotation_Y = 0f;

    private void TransRotation()
    {
        float camera_X = InputSystem.Instance.Looking.x * -1f;
        float camera_Y = InputSystem.Instance.Looking.y * 1f;

        rotation_Y += camera_X;
        rotation_X -= camera_Y;

        rotation_X = Mathf.Clamp(rotation_X, -80f, 80f);

        cameraAim.rotation = Quaternion.Euler(rotation_X, rotation_Y, 0);
    }

    private void Walking()
    {
        slimeCharacterBase.Walking();
    }

    private void Running()
    {
        slimeCharacterBase.Run();
    }

    private void RightShiftLocked()
    {
        if(slimeCharacterBase.OnLeftShiftHold == false)
        {
            slimeCharacterBase.OnLeftShiftHold = true;
        }
        else
        {
            slimeCharacterBase.OnLeftShiftHold = false;
        }
    }
    
    private void Jumping()
    {
        
    }
}
