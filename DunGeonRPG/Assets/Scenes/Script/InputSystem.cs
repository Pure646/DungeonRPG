using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance { get; private set; }

    public Vector2 Movement => movement;
    private Vector2 movement;

    public Vector2 Looking => looking;
    private Vector2 looking;

    public System.Action Jump;
    public System.Action RightShiftHoldingLocked;
    public System.Action LeftShiftRunning;
    public System.Action Walk;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        float MoveVec_X = Input.GetAxis("Horizontal");
        float MoveVec_Y = Input.GetAxis("Vertical");
        movement = new Vector2(MoveVec_X, MoveVec_Y);

        float Look_X = Input.GetAxis("Mouse X");
        float Look_Y = Input.GetAxis("Mouse Y");
        looking = new Vector2(Look_X, Look_Y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            RightShiftHoldingLocked?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            LeftShiftRunning?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Walk?.Invoke();
        }
    }
    
}
