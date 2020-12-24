using UnityEngine;

public class DemoCameraController : MonoBehaviour
{
    public InputMaster controls;
    public CameraBehaviour cameraScript;

    private void Awake()
    {
        cameraScript = Camera.main.GetComponent<CameraBehaviour>();
        controls = new InputMaster();
        controls.Player.Interact.performed += ctx => cameraScript.TriggerFocusChange();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
