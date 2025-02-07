using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SlimeCharacterBase : MonoBehaviour
{
    private Animator animator;
    private SlimeCharacterController slimeCharacterController;
    private CharacterController characterController;
    private Vector2 movement;
    private float horizontalBlend;
    private float verticalBlend;
    private float animatorRun;
    private bool OnJump;

    public float runSpeed = 2f;
    public bool OnLeftShiftHold = false;   //false : 달리기 눌렀다가 떗다가 할지 / true : 달리기 상태에서 다시 LeftShift를 누를 때까지 달리기가 유지 될지의 여부
    public bool OnRun;

    private float Verticalverocity = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        slimeCharacterController = GetComponent<SlimeCharacterController>();
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        SlimeRunning();
        JumpAndGravity();
    }

    public void Move(Vector2 Input)
    {
        movement = ((transform.right * Input.x) + (transform.forward * Input.y)) * runSpeed;
        horizontalBlend = Mathf.Lerp(horizontalBlend, Input.x, 10 * Time.deltaTime);
        verticalBlend = Mathf.Lerp(verticalBlend, Input.y, 10 * Time.deltaTime);
        
        characterController.Move(movement);

        animator.SetFloat("Horizontal", horizontalBlend);
        animator.SetFloat("Vertical", verticalBlend);
        animator.SetFloat("Magnitude", movement.magnitude);
        animator.SetFloat("Run", animatorRun);
    }
    public void SlimeRunning()
    {
        if(OnRun)
        {
            animatorRun = 1f;
            runSpeed = 2f;
        }
        else
        {
            animatorRun = 0f;
            runSpeed = 1f;
        }
    }

    public void Walking()
    {
        if (OnLeftShiftHold == false)
        {
            OnRun = false;
        }
        else if(OnLeftShiftHold == true)
        {
            return;
        }
    }

    public void Run()
    {
        if (OnLeftShiftHold == false)
        {
            OnRun = true;
        }
        else if (OnLeftShiftHold == true)
        {
            if (OnRun == false)
            {
                OnRun = true;
            }
            else
            {
                OnRun = false;
            }
        }
    }

    public void Jump()
    {
        OnJump = true;
    }
    private void JumpAndGravity()
    {

    }
}
