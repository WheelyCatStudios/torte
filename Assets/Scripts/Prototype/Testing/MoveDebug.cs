using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDebug : MonoBehaviour
{

    Rigidbody2D rigid;

    [Range(-1,1)]
    public float x;
    [Range(-1,1)]
    public float y;

    public float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Debug.Log("X Input: " + x);
        Debug.Log("Y Input: " + y);
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(x, y) * speed;
    }
}
