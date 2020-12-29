using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CameraBehaviour {
[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
	#region attribute
    private Vector3 targetPosition;
    private Vector3 dampVelocity = Vector3.zero;

    private Transform target;

    private BoundsInt tilemapBoundaries;

    private Vector2 clampValuesX;
    private Vector2 clampValuesY;

    private Camera mainCamera;
    private Vector2 cameraSize;

	//This is public so you can do things like block player movement in case is not the focus
    public bool FocusingPrimary => target == primaryTarget;
    private bool ReturnTimerIsActive => timeUntilReturn > 0;


	#region inspectable attributes
    [Header("Tilemap Settings")]
	[SerializeField]
    private Tilemap tilemap;

    [Header("Tracking Settings")]
	[SerializeField]
    private Transform primaryTarget;
    [Space]


    [Tooltip("A bigger number means a longer time for the camera to reach the target")]
    [Range(0,35)]
	[SerializeField]
    private float smoothTime;

    [Header("Changing Focus")]
	[SerializeField]
    private Transform secondaryTarget;

    [Tooltip("Time (in Seconds) in which the camera will be focusing on secondary target")]
	[SerializeField]
    private float timeUntilReturn;
	#endregion inspectable attributes
	#endregion Attribute

	#region utility
    private Vector2 GetCameraSize => new Vector2(mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);

	//Putting this in a function just in case the tilemap or the camera size change on Runtime
    private void CalculateClampValues()
    {
        clampValuesX = new Vector2(tilemapBoundaries.xMin + cameraSize.x, tilemapBoundaries.xMax - cameraSize.x);
        clampValuesY = new Vector2(tilemapBoundaries.yMin + cameraSize.y, tilemapBoundaries.yMax - cameraSize.y);
    }

	private bool TargetTooClose => Vector2.Distance(target.position, transform.position) < 0.1f;
	#endregion utility

	#region MonoBehaviour
    private void Start()
    {
        target = primaryTarget;

        mainCamera = GetComponent<Camera>();

        cameraSize = GetCameraSize;
        tilemapBoundaries = tilemap.cellBounds;

        CalculateClampValues();
    }

	void FixedUpdate()
    {
        if (TargetTooClose) return;

        print(target.position);

        //Restrict the camera position to Map's Boundaries
        targetPosition = new Vector3(Mathf.Clamp(target.position.x, clampValuesX.x, clampValuesX.y), Mathf.Clamp(target.position.y, clampValuesY.x, clampValuesY.y), transform.position.z);
        
        //Moving the camera to target's position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref dampVelocity, smoothTime * Time.deltaTime);
    }
	#endregion MonoBehaviour

	#region methods
    public void TriggerFocusChange()
    {
        if(!FocusingPrimary && !ReturnTimerIsActive)
            ReturnFocusToPrimary();
        else if (FocusingPrimary)
            StartCoroutine(ChangeTargetFocus(secondaryTarget, timeUntilReturn));
    }

    private void ReturnFocusToPrimary() => target = primaryTarget;

	private void SetFocusTarget(Transform newTarget) => target = newTarget;

    IEnumerator ChangeTargetFocus(Transform newTarget, float returnTime = 0)
    {
        if(newTarget != null)
        {
            SetFocusTarget(newTarget);
            if (returnTime > 0)
            {
                yield return new WaitForSeconds(timeUntilReturn);
                ReturnFocusToPrimary();
            }
        }
    }
	#endregion methods
}
}