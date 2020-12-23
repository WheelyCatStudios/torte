using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controlls the movement of any given entity.
/// Also provides user control to an entity via <code>InputMaster</code>
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovmentScript : MonoBehaviour
{
	#region attributes
	/// <summary>
	/// Local reference to the neighboring rigid body on this entity.
	/// </summary>
    private Rigidbody2D rb;

    public InputMaster controls;
	/// <summary>
	/// The user input collector
	/// </summary>

    [Header("Variables")]
    
	/// <summary>
	/// This entities current direction
	/// </summary>
    private Vector2 direction = Vector2.zero;
    public float speed = 1f;
    public float runSpeedModifer = 2f;

	/// <summary>
	/// This entities current speed
	/// </summary>
	/// <summary>
	/// The speed modifyer for running.
	/// <code>runningSpeed = (speed * runSpeedMod)</code>
	/// </summary>
	/// <summary>
	/// Current speed modifier
	/// <code>actualspeed = (speed * runSpeedMod)</code>
	/// </summary>
    private float currentRunSpeedModifer = 1f;

    public float maxSpeed = 0f; // Will defalt to speed*runSpeedModifer

    public float timeOfAcceleration = 2.5f;

	/// <summary>
	/// The maximum speed at which this entity can travel
	/// </summary>
	/// <summary>
	/// The time that will be taken to accelerate
	/// </summary>
	/// <summary>
	/// Rate of acceleration between idle to walking
	/// </summary>
    private float accelRateIdleToWalk;

	/// <summary>
	/// rate of accelleration between walking to running
	/// </summary>
    private float accelRateWalkToRun;
    // Video link https://www.youtube.com/watch?v=uVKHllD-JZk
	#endregion
    

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
	/// <summary>
	/// Enables the entire input master when this monobehavior is enabled
	/// </summary>
	/// <summary>
	/// Disables the entire input master when this monobehavior is disabled
	/// </summary>
	/// <summary>
	/// Disables all player controls
	/// </summary>
	/// <summary>
	/// Disables all player controls
	/// </summary>
	/// <summary>
	/// Enables the player's ability to use movement controls
	/// Other controlls are uneffected
	/// </summary>
	/// <summary>
	/// Disables the player's ability to use movement controls
	/// Other controlls are uneffected
	/// </summary>
	/// <summary>
	/// Enables / Disables all player control via the input master according to <paramref name="enable"/>
	/// </summary>
	/// <param name="enable">If true, input master is enabled, otherise disabled</param>
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
	/// <summary>
	/// Enables / Disables player's ability to control movement via the input master according to <paramref name="enable"/>
	/// Other controlls are uneffected
	/// </summary>
	/// <param name="disable"></param>
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
	/// <summary>
	/// Replaces any current velicity with <code><paramref name="direction"/> * speed * currentRunSpeedModifyer </code>
	/// </summary>
	/// <param name="direction">Direction we should move</param>
    {
        if (disable)
            controls.Player.Movment.Disable();
        else
            controls.Player.Movment.Enable();
		// TODO : Commented code should not pass through pull request.
    }
}