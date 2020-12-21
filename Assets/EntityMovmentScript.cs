using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityMovmentScript : MonoBehaviour
{
    public MasterInput controls = new MasterInput();


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        controls.Player.Movment.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }
    
    private void OnEnable() {
        controls.Enable();
    }
    private void OnDisable() {
        controls.Disable();
    }
    void Move(Vector2 direction)
    {
        Debug.Log("Move " + direction);
    }
}
