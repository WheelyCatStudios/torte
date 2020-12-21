using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityMovmentScript : MonoBehaviour
{
    public InputMaster controls;
    public Rigidbody2D rb;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        controls = new InputMaster();
        controls.Player.Movment.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }
    
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
