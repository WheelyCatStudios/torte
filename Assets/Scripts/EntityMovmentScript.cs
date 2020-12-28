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

	/// <summary>
	/// The user input collector
	/// </summary>
    private InputMaster controls;

    [Header("Variables")]
    
	/// <summary>
	/// This entities current direction
	/// </summary>
    private Vector2 direction = Vector2.zero;

	/// <summary>
	/// This entities current speed
	/// </summary>
	[SerializeField]
	[Tooltip("The entities current speed")]
    private float speed = 1f;

	/// <summary>
	/// The speed modifyer for running.
	/// <code>runningSpeed = (speed * runSpeedMod)</code>
	/// </summary>
	[SerializeField]
	[Tooltip("The speed modifer for running")]
    private float runSpeedModifer = 2f; //TODO Reenable run behavior

	/// <summary>
	/// Current speed modifier
	/// <code>actualspeed = (speed * runSpeedMod)</code>
	/// </summary>
    private float currentRunSpeedModifer = 1f;

	/// <summary>
	/// The maximum speed at which this entity can travel
	/// </summary>
	[SerializeField]
    private float maxSpeed = 0f; // Will defalt to speed*runSpeedModifer
	
	/// <summary>
	/// The time that will be taken to accelerate
	/// </summary>
	[SerializeField]
    private readonly float timeOfAcceleration = 2.5f;

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

	#region construction

	/// <summary>
	/// MonoBehaviour constructor replacement
	/// </summary>
    void Awake()
    {
        PopulateAttributes();
		RegisterInputs();
	}

	/// <summary>
	/// Populates maxSpeed, Acceleration rates, 
	/// </summary>
	private void PopulateAttributes() {
		rb = GetComponent<Rigidbody2D>();	//Get Components
		controls = new InputMaster(); 				// Create Input Master

        //Set maxSpeed (if not already set)
        maxSpeed = (maxSpeed > 0) ? maxSpeed : (speed * runSpeedModifer);
		
        //Calucate Acceleration
        accelRateIdleToWalk = speed / timeOfAcceleration;
        accelRateWalkToRun = ((speed * runSpeedModifer) - speed) / timeOfAcceleration;
	}

	/// <summary>
	/// Registers user input hooks for movement start / stop
	/// </summary>
	private void RegisterInputs(){
        //Register Inputs for Movment
        controls.Player.Movement.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => direction = Vector2.zero;

        //Register Inputs for Run
        /*controls.Player.Run.performed += ctx => setSpeedModifer(runSpeedModifer);
        controls.Player.Run.canceled += ctx => setSpeedModifer(1.0f);*/
	}

	#endregion
    
	#region MonoBehaviour
	
    /// <summary>
    /// MonoBehaviour framely update
    /// </summary>
    // void Update() =>

    private void FixedUpdate() => Move(direction);

	#region enable/disable
	/// <summary>
	/// Enables the entire input master when this monobehavior is enabled
	/// </summary>
    private void OnEnable() => controls.Enable();

	/// <summary>
	/// Disables the entire input master when this monobehavior is disabled
	/// </summary>
    private void OnDisable() => controls.Disable();
	
	/// <summary>
	/// Disables all player controls
	/// </summary>
    public void EnableControl() => setEnableControl(true);
	
	/// <summary>
	/// Disables all player controls
	/// </summary>
	public void DisableControl() => setEnableControl(false);

	/// <summary>
	/// Enables the player's ability to use movement controls
	/// Other controlls are uneffected
	/// </summary>
	public void EnableMovementControl() => setEnableMovementControl(true);

	/// <summary>
	/// Disables the player's ability to use movement controls
	/// Other controlls are uneffected
	/// </summary>
	public void DisableMovementControl() => setEnableMovementControl(false);
    
	/// <summary>
	/// Enables / Disables all player control via the input master according to <paramref name="enable"/>
	/// </summary>
	/// <param name="enable">If true, input master is enabled, otherise disabled</param>
    public void setEnableControl(bool enable)
    {
        if (enable)
            controls.Player.Enable();
        else
            controls.Player.Disable();
	}
	
	/// <summary>
	/// Enables / Disables player's ability to control movement via the input master according to <paramref name="enable"/>
	/// Other controlls are uneffected
	/// </summary>
	/// <param name="disable"></param>
	public void setEnableMovementControl(bool disable)
    {
        if (disable)
            controls.Player.Movement.Disable();
        else
            controls.Player.Movement.Enable();
    }

	#endregion enable/disable
	#endregion MonoBehaviour

	#region Movement
	/// <summary>
	/// Immediately replaces any current velicity on the attached rigidbody with <code><paramref name="direction"/> * speed * currentRunSpeedModifyer </code>
	/// </summary>
	/// <param name="direction">Direction we should move</param>
    public void Move(Vector2 direction) => 
        rb.velocity = direction * speed * currentRunSpeedModifer;

    public void setSpeedModifer(float speed) => currentRunSpeedModifer = speed;

	#endregion Movement
}