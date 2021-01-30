using UnityEngine;

namespace CameraBehaviour {
public class DemoCameraController : MonoBehaviour
{
    public InputMaster controls;
    public CameraBehaviour cameraScript;

    private void Awake()
    {
        cameraScript = Camera.main.GetComponent<CameraBehaviour>();
        controls = new InputMaster();
        controls.Player.Interaction.performed += ctx => cameraScript.TriggerFocusChange();
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
}