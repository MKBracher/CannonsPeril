using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speed;

    private Collider2D coll;
    private bool facingLeft = false;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (facingLeft)
        {
            //test to see if beyond left cap
            if (transform.position.x > leftCap)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            else
            {
                facingLeft = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        else
        {
            if (transform.position.x < rightCap)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

            else
            {
                facingLeft = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }




}
