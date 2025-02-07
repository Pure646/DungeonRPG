using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCharacterController : MonoBehaviour
{
    private SlimeCharacterBase slimeCharacterBase;
    private Vector2 movement;
    private void Awake()
    {
        slimeCharacterBase = GetComponent<SlimeCharacterBase>();
    }
    private void Start()
    {
        movement = InputSystem.Instance.Movement;

        InputSystem.Instance.Jump += Jumping;
        InputSystem.Instance.RightShiftHoldingLocked += RightShiftLocked;
        InputSystem.Instance.LeftShiftRunning += Running;
        InputSystem.Instance.Walk += Walking;
    }

    private void Update()
    {
        Moving();
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
    private void Moving()
    {
        slimeCharacterBase.Move(movement);
    }
}
