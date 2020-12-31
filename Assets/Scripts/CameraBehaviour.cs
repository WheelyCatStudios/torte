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
	private bool isTargetTooClose() => Vector2.Distance(target.position, transform.position) < 0.1f;

	//Putting this in a function just in case the tilemap or the camera size change on Runtime
    private void CalculateClampValues()
    {
        if (!tilemap) return;
        clampValuesX = new Vector2(tilemapBoundaries.xMin + cameraSize.x, tilemapBoundaries.xMax - cameraSize.x);
        clampValuesY = new Vector2(tilemapBoundaries.yMin + cameraSize.y, tilemapBoundaries.yMax - cameraSize.y);
    }

	#endregion utility

	#region MonoBehaviour
    private void Start()
    {
        SetFocusToPrimary();

        mainCamera = GetComponent<Camera>();

        SetCameraSize();
        if(tilemap) tilemapBoundaries = tilemap.cellBounds;

        CalculateClampValues();
    }

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
    public void TriggerFocusChange()
    {
        if(!FocusingPrimary && !ReturnTimerIsActive)
            SetFocusToPrimary();
        else if (FocusingPrimary)
            StartCoroutine(ChangeTargetFocus(secondaryTarget, timeUntilReturn));
    }
    private Vector2 SetCameraSize() => cameraSize = new Vector2(mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);

    private void SetFocusToPrimary() => SetFocusTarget(primaryTarget);

	private void SetFocusTarget(Transform newTarget) => target = newTarget;

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