using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityMovmentScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public InputMaster controls;


    [Header("Variables")]
    public float speed = 1f;


    void Awake()
    {
        //Get Components
        rb = GetComponent<Rigidbody2D>();
        //Create Input
        controls = new InputMaster();

        //Register Inputs for Movment
        controls.Player.Movment.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Movment.canceled += ctx => Move(Vector2.zero);
        //Register Inputs for Run
        
    }


    private void OnEnable()
    {
        //Enable Controlls
        controls.Enable();
    }
    private void OnDisable()
    {
        //Disable Controlls
        controls.Disable();
    }

    public void Move(Vector2 direction)
    {
        //Setting direction to rigidbody Immediatly
        rb.velocity = direction * speed;

        //Old Code (Doesnt work as Input Event is not called in a Update Loop)
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    //Enable Player Contorl
    public void EnableControl()
    {
        controls.Player.Enable();
    }
    public void EnableControl(bool enable)
    {
        if (enable)
            controls.Player.Enable();
        else
            controls.Player.Disable();
    }

    //Disable Player Control
    public void DisableControl()
    {
        controls.Player.Disable();
    }
    public void DisableControl(bool disable)
    {
        if (disable)
            controls.Player.Disable();
        else
            controls.Player.Enable();
    }
    
    //Enable Movement Controlls
    public void EnableMovement()
    {
        controls.Player.Movment.Enable();
    }
    public void EnableMovement(bool enable)
    {
        if (enable)
            controls.Player.Movment.Enable();
        else
            controls.Player.Movment.Disable();
    }

    //Disable Movement Controlls
    public void DisableMovement()
    {
        controls.Player.Movment.Disable();
    }
    public void DisableMovement(bool disable)
    {
        if (disable)
            controls.Player.Movment.Disable();
        else
            controls.Player.Movment.Enable();
    }
}