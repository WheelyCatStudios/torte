using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public InputMaster controls;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private float speed = 10;

    private Vector2 direction = Vector2.zero;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled += ctx => Move(ctx.ReadValue<Vector2>());
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Move(Vector2 inputDirection)
    {
        direction = inputDirection;

        if (direction != Vector2.zero)
        {
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
        }
        anim.SetFloat("Speed", direction.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.velocity = direction.normalized * speed;
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
