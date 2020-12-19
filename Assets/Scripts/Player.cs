using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private float speed = 10;

    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(direction != Vector2.zero)
        {
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
        }
        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.velocity = direction.normalized * speed;
    }
}
