using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SlimeCharacterBase : MonoBehaviour
{
    private Animator animator;
    private SlimeCharacterController slimeCharacterController;
    private CharacterController charactercontroller;
    private Vector2 movement;

    private float animatorRun;
    private bool OnJump;
    [SerializeField] private float Runspeed;
    [SerializeField] private float Walkspeed;
    private float currentSpeed;

    public bool OnLeftShiftHold = false;   //false : 달리기 눌렀다가 떗다가 할지 / true : 달리기 상태에서 다시 LeftShift를 누를 때까지 달리기가 유지 될지의 여부
    public bool OnRun;

    private float Verticalverocity = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        slimeCharacterController = GetComponent<SlimeCharacterController>();
        charactercontroller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        SlimeRunning();
        Gravity();
        Move();
    }
    [SerializeField] private LayerMask layer;
    [SerializeField] private float ray_maxdistance = 0.05f;
    //private Collider[] GroundCollider;
    private bool InGrounded;
    private float gravity = -9.81f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0f, ray_maxdistance, 0f));
    }
    private void Gravity()
    {
        InGrounded = Physics.Raycast(transform.position, transform.position - Vector3.up, ray_maxdistance, layer, QueryTriggerInteraction.Ignore);
        if(InGrounded)
        {
            gravity = 0f;
        }
        else
        {
            gravity = -9.81f;
            Vector3 FallsGravity = Vector3.up * gravity * Time.deltaTime / 2;
            charactercontroller.Move(FallsGravity);
        }
    }


    private void Rotate()
    {

    }

    private void Move()
    {
        movement.x = InputSystem.Instance.Movement.x;
        movement.y = InputSystem.Instance.Movement.y;
        Vector3 movec = (transform.forward * movement.y + transform.right * movement.x) * currentSpeed * Time.deltaTime;
        charactercontroller.Move(movec);

        if(OnRun)
        {
            currentSpeed = Runspeed;
        }
        else
        {
            currentSpeed = Walkspeed;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        animator.SetFloat("Run", animatorRun);
    }
    public void SlimeRunning()
    {
        if(OnRun)
        {
            animatorRun = 1f;
        }
        else
        {
            animatorRun = 0f;
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

    private void Jumping()
    {

    }
}
