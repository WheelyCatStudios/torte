using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityMovmentScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public InputMaster controls;

    [Header("Variables")]
    
    private Vector2 direction = Vector2.zero;
    public float speed = 1f;
    public float runSpeedModifer = 2f;

    private float currentRunSpeedModifer = 1f;

    public float maxSpeed = 0f; // Will defalt to speed*runSpeedModifer

    public float timeOfAcceleration = 2.5f;

    private float accelRateIdleToWalk;
    private float accelRateWalkToRun;

    void Awake()
    {
        //Get Components
        rb = GetComponent<Rigidbody2D>();
        //Create Input
        controls = new InputMaster();

        //Register Inputs for Movment
        controls.Player.Movment.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Player.Movment.canceled += ctx => direction = Vector2.zero;
        //Register Inputs for Run
        controls.Player.Run.performed += ctx => Run(runSpeedModifer);
        controls.Player.Run.canceled += ctx => Run(1.0f);

        //Set maxSpeed (if not already set)
        maxSpeed = (maxSpeed > 0) ? maxSpeed : (speed * runSpeedModifer);

        //Calucate Acceleration
        accelRateIdleToWalk = speed / timeOfAcceleration;
        accelRateWalkToRun = ((speed * runSpeedModifer) - speed) / timeOfAcceleration;
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Debug.Log(rb.velocity);
    }

    private void FixedUpdate() {
        Move(direction);
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
        rb.velocity = direction * speed * currentRunSpeedModifer;
        //Old Code (Doesnt work as Input Event is not called in a Update Loop)
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public void Run(float speed)
    {
        currentRunSpeedModifer = speed;
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