using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityMovmentScript : MonoBehaviour
{
    public MasterInput controls;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        controls = new MasterInput();
        controls.Player.Movment.performed += ctx => Move();
    }
    
    private void OnEnable() {
        controls.Enable();
    }
    private void OnDisable() {
        controls.Disable();
    }
    void Move()
    {
        Debug.Log("Move ");
    }
}
