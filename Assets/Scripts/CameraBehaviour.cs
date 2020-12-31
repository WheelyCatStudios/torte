using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The main controller for the player's camera
/// 
/// Requires a Sibling Camera component
/// </summary>
namespace CameraBehaviour {
[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
	#region attributes

	#region inspectable attributes
	/// <summary>
	/// The tilemap which this camera is observing
	/// <br/>
	/// Used to determine the movement limits of the camera. <br/>
	/// <br/>
	/// This is not dynamically located, and must be set in the inspector.
	/// </summary>
    [Header("Tilemap Settings")]
	[SerializeField]
    private Tilemap tilemap;

    [Space]

	/// <summary>
	/// The time taken to move from the current position to the target position.
	/// <br/>
	/// The bigger the value, the more time taken to reach the target.
	/// </summary>
    [Tooltip("A bigger number means a longer time for the camera to reach the target")]
    [Range(0,35)]
	[SerializeField]
    private float smoothTime;

    [Header("Changing Focus")]

	/// <summary>
	/// Determines how much longer is left on a timed temporary focus
	/// <br/>
	/// When focusing on a temporary target, once this timer reaches 0, the target will be reset to <a cref="primaryTarget"/>
	/// </summary>
    [Tooltip("Time (in Seconds) in which the camera will be focusing on secondary target")]
	[SerializeField]
    private float timeUntilReturn;

	#endregion inspectable attributes

	#region targeting
	/// <summary>
	/// Main object of interest
	/// <br/>
	/// This is centeral point of interest, i.e the player. The camera may not always be focusing on this object.
	/// </summary>
    [Header("Tracking Settings")]
	[SerializeField]
    private Transform primaryTarget;

	/// <summary>
	/// A temporary object of focus
	/// <br/>
	/// Can be used to point the player to specific object. Can be used in conjunction with <a cref="timeUntilReturn"/> to temporarily focus on an object, then return.
	/// <see cref="ChangeTargetFocus"/>
	/// </summary>
	[SerializeField]
    private Transform secondaryTarget;

	/// <summary>
	/// True object of focus
	/// <br/>
	/// This is the object that the camera will move to transfer focus too. 
	/// <br/>
	/// May be different from <a cref="primaryTarget"/>.
	/// <br/>
	/// If <a cref="target"/> != <a cref="primaryTarget"/> and a timer is active, the target will automatically be reset to <a cref="primaryTarget"/> once the timer reaches 0.
	/// </summary>
    private Transform target;

	/// <summary>
	/// Vector position of true camera focus
	/// <br/>
	/// This is the real focal position of the camera, 
	/// representing the position of interest NOT the true position of the camera.
	/// <br/>
	/// This can differ from the position of <a cref="target"/>,
	/// </summary>
    private Vector3 targetPosition;

	/// <summary>
	/// Dampening velocity for tracking movement
	/// <br/>
	/// Used to create the smooth velocity responsible for the rubber banding effect when the camera moves to <a cref="targetPosition"/>
	/// </summary>
    private Vector3 dampVelocity = Vector3.zero;
	#endregion targeting

	#region Boundries
	/// <summary>
	/// The boundries of the containing tile map
	/// <br/>
	/// Used to limit the cameras position to within the bounds of the tilemap
	/// </summary>
    private BoundsInt tilemapBoundaries;

	/// <summary>
	/// Max and Min values that represent the full width of travel the
	/// camera may follow on the X axis.
	/// <br/>
	/// Where <a cref="clampValuesX.x"/> represents the left most position the camera may be, and  <a cref="clampValuesX.y"/> represents the right.
	/// <br/>
	/// Used by <see cref="Mathf.Clamp(camX, clampValuesX.x, clampValuesX.y)"/> to assert that the camera remains within the bounds of the map
	/// </summary>
    private Vector2 clampValuesX;

	/// <summary>
	/// Max and Min values that represent the full height of travel the
	/// camera may follow on the Y axis.
	/// <br/>
	/// Where <a cref="clampValuesY.x"/> represents the top most position the camera may be, and  <a cref="clampValuesX.y"/> represents the bottom most.
	/// <br/>
	/// Used by <see cref="Mathf.Clamp(camY, clampValuesY.x, clampValuesY.y)"/> to assert that the camera remains within the bounds of the map
	/// </summary>
    private Vector2 clampValuesY;
	#endregion Boundries

	/// <summary>
	/// A local reference to the sibiling <a cref="Camera"/> component that this script controls
	/// <br/>
	/// Found dynamically on <a cref="MonoBehaviour.Start()"/>
	/// </summary>
    private Camera mainCamera;

	/// <summary>
	/// The size of the camera's viewport
	/// <br/>
	/// Used to ensure the camera's entire view remains within the bounds of the tilemap, not just the camera object itself
	/// </summary>
    private Vector2 cameraSize;
	#endregion

	#region utility
	/// <summary>
	///	Determines if the camera is focusing on the primary target
	/// <br/>
	/// This is public so you can do things like block player movement in case is not the focus
	/// </summary>
	/// <returns>true if the current target is the primary target</returns>
    public bool isFocusingPrimary() => target == primaryTarget;

	/// <summary>
	/// Determines if the camera is temporarily focusing on a secondary target, and is running a timer to return to primary focus.
	/// </summary>
	/// <returns>true if <a cref="timeUntilReturn"/> is greater than 0</returns>
    private bool isTimerActive() => timeUntilReturn > 0;

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	private bool isTargetTooClose() => Vector2.Distance(target.position, transform.position) < 0.1f;

	//Putting this in a function just in case the tilemap or the camera size change on Runtime
	/// <summary>
	/// Populates <a cref="clampValuesX"/> and <a cref="clampValuesX"/>
	/// <br/>
	/// Calculates the true bounds that the camera must remain within such that the camera's view port stays within the bounds of the map.
	/// </summary>
    private void CalculateClampValues()
    {
        if (!tilemap) return;
        clampValuesX = new Vector2(tilemapBoundaries.xMin + cameraSize.x, tilemapBoundaries.xMax - cameraSize.x);
        clampValuesY = new Vector2(tilemapBoundaries.yMin + cameraSize.y, tilemapBoundaries.yMax - cameraSize.y);
    }

	#endregion utility

	#region MonoBehaviour
	/// <summary>
	/// Monobehaviour start
	/// <br/>
	/// Configures this component on parent GameObject instantiation
	/// <br/>
	/// <list type="bullet">
	/// 	<listheader>
	/// 		 <term>Start Routine</term>
	/// 	</listheader>
	/// 
	/// 	<item>Focuses on the primary object</item>
	/// 	<item>Dynamically populates <a cref="mainCamera"/> by finding a sibling camera component</item>
	/// 	<item>Set's the viewport size</item>
	/// 	<item>Calculates tilemap boundaries</item>
	/// 	<item>Calculates the camera's true boundaries. See <see cref="CalculateClampValues"/></item>
	/// </list>
	/// </summary>
    private void Start()
    {
        SetFocusToPrimary();

        mainCamera = GetComponent<Camera>();

        SetCameraSize();
        if(tilemap) tilemapBoundaries = tilemap.cellBounds;

        CalculateClampValues();
    }

	/// <summary>
	/// MonoBehaviour update
	/// <br/>
	/// Performs camera movement based on current, target, bounds, and camera position.
	/// </summary>
	void FixedUpdate()
    {
        if (isTargetTooClose()) return;

        targetPosition = target.position;

        //Restrict the camera position to Map's Boundaries if tileset is set
        if(tilemap)
            targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, clampValuesX.x, clampValuesX.y), Mathf.Clamp(targetPosition.y, clampValuesY.x, clampValuesY.y));

        targetPosition.z = transform.position.z;

        //Moving the camera to target's position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref dampVelocity, smoothTime * Time.deltaTime);
    }
	#endregion MonoBehaviour

	#region methods
	/// <summary>
	/// Request the camera to change focus between primary and secondary targets
	/// <br/>
	/// <list type="bullet">
	/// 	<item>If is not focusing on primary AND there is no return timer, camera will return to primary.</item>
	/// 	<item>If is focusing on primary, will switch to secondary</item>
	/// </list>
	/// </summary>
    public void TriggerFocusChange()
    {
        if(!isFocusingPrimary() && !isTimerActive())
            SetFocusToPrimary();
        else if (isFocusingPrimary())
            StartCoroutine(ChangeTargetFocus(secondaryTarget, timeUntilReturn));
    }

	/// <summary>
	/// Populates <a cref="cameraSize"/> with the calculated orthagraphic viewport size
	/// </summary>
	/// <returns>The value stored in <a cref="cameraSize"/> after calculation</returns>
    private Vector2 SetCameraSize() => cameraSize = new Vector2(mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);

	/// <summary>
	/// Sets <a cref="target"/> to <a cref="primartTarget"/>
	/// </summary>
    private void SetFocusToPrimary() => SetFocusTarget(primaryTarget);

	/// <summary>
	/// Sets <a cref="target"/> to <paramref name="newTarget"/>
	/// </summary>
	/// <param name="newTarget">The new target to track</param>
	private void SetFocusTarget(Transform newTarget) => target = newTarget;

	/// <summary>
	/// Performs camera focus change to <paramref name="newTarget"/>
	/// </summary>
	/// <param name="newTarget">The GameObject to focus on</param>
	/// <param name="returnTime">The time after which the focus should be returned to <a cref="primartTarget"/></param>
	/// <returns></returns>
    IEnumerator ChangeTargetFocus(Transform newTarget, float returnTime = 0)
    {
        if(newTarget != null)
        {
            SetFocusTarget(newTarget);
            if (returnTime > 0)
            {
                yield return new WaitForSeconds(timeUntilReturn);
                SetFocusToPrimary();
            }
        }
    }
	#endregion methods
}
}