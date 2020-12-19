using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 dampVelocity = Vector3.zero;
    private Transform target;

    private BoundsInt tilemapBoundaries;

    private Camera mainCamera;
    private Vector2 cameraSize;

    [Header("Tilemap Settings")]
    public Tilemap tilemap;

    [Header("Tracking Settings")]
    public Transform primaryTarget;
    [Space]

    [Tooltip("A bigger number means a longer time for the camera to reach the target")]
    [Range(0,35)]
    public float smoothTime;

    [Header("Changin Focus")]
    public Transform secondaryTarget;

    [Tooltip("Time (in Seconds) in which the camera will be focusing on secondary target")]
    public float timeUntilReturn;

    private void Start()
    {
        target = primaryTarget;

        mainCamera = GetComponent<Camera>();
        cameraSize = new Vector2(mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);

        tilemapBoundaries = tilemap.cellBounds;
    }

    public bool FocusingPrimary => target == primaryTarget;
    public bool ReturnTimerIsActive => timeUntilReturn > 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(!FocusingPrimary && !ReturnTimerIsActive)
            {
                ReturnFocusToPrimary();
            }
            else if (FocusingPrimary)
            {
                StartCoroutine(ChangeTargetFocus(secondaryTarget, timeUntilReturn));
            }
            
        }
    }

    public void ReturnFocusToPrimary()
    {
        target = primaryTarget;
    }

    IEnumerator ChangeTargetFocus(Transform newTarget, float returnTime = 0)
    {
        if(newTarget != null)
        {
            target = newTarget;
            if (returnTime > 0)
            {
                yield return new WaitForSeconds(timeUntilReturn);
                target = primaryTarget;
            }
        }
    }


    public bool TargetTooClose => Vector2.Distance(target.position, transform.position) < 0.1f;

    void FixedUpdate()
    {
        if (TargetTooClose) return;

        targetPosition = new Vector3(target.position.x, target.position.y, -10f);

        targetPosition.x = Mathf.Clamp(targetPosition.x, tilemapBoundaries.xMin + cameraSize.x, tilemapBoundaries.xMax - cameraSize.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, tilemapBoundaries.yMin + cameraSize.y, tilemapBoundaries.yMax - cameraSize.y);
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref dampVelocity, smoothTime * Time.deltaTime);
    }
}
