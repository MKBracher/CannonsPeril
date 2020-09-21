using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    public bool holdingCannon;
    public cannonPickup cannon;
    public bool wait;
    public GameObject cannonSpawner;
    private Spawner spawnScript;

    //FSM
    private enum State {idle, running, jumping, falling, hurt, cannonIdle, cannonRunning, cannonJumping, cannonFalling, cannonHurt}
    private State state = State.cannonIdle;




    //inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float hurtForce = 10f;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        holdingCannon = false;
        state = State.idle;
        spawnScript = cannonSpawner.GetComponent<Spawner>();
       
    }

    private void Update()
    {

        if (state != State.hurt && state != State.cannonHurt)
        {
            Movement();
        }



        //If the player is holding the cannon and presses e, drops the cannon
        if (Input.GetKeyDown(KeyCode.E) && holdingCannon && wait)
        {
            spawnScript.disabled = false;
            spawnScript.setActiveStatus();
            cannon.Drop();

        }
        wait = true;



        animationState();
        anim.SetInteger("state", (int)state); //sets animation based on enumerator state

    }

    public void cannonState()
    //Swaps cannon and no cannon, called when the cannon is dropped or picked up
    {
        if (holdingCannon)
        {
            holdingCannon = false;
            state = State.idle;
            wait = false;
            speed = 5f;
            jumpForce = 20f;
            hurtForce = 10f;

        }

        else
        {
            holdingCannon = true;
            state = State.cannonIdle;
            wait = false;
            speed = 3f;
            jumpForce = 10f;
            hurtForce = 5f;

        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" )
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling || state == State.cannonFalling)
            {
                enemy.JumpedOn();
                Jump();
            }

            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to right - damaged and moved left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);


                }
                else
                {
                    //Enemy is to left - damaged and moved right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }

        }
    }
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");


        //moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        //moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }


    private void animationState()
    {
        if (!holdingCannon)
        {
            if (state == State.jumping)
            {
                if (rb.velocity.y < .1f)
                {
                    state = State.falling;
                }
            }

            else if (state == State.falling)
            {
                if (coll.IsTouchingLayers(ground))
                {
                    state = State.idle;
                }
            }

            else if (state == State.hurt)
            {
                if (Mathf.Abs(rb.velocity.x) < .1f)
                {
                    state = State.idle;
                }
            }


            else if (Mathf.Abs(rb.velocity.x) > 2f)
            {
                //Moving
                state = State.running;
            }

            else
            {
                state = State.idle;
            }
        }

        else
        {
            if (state == State.cannonJumping)
            {
                if (rb.velocity.y < .1f)
                {
                    state = State.cannonFalling;
                }
            }

            else if (state == State.cannonFalling)
            {
                if (coll.IsTouchingLayers(ground))
                {
                    state = State.cannonIdle;
                }
            }

            else if (state == State.cannonHurt)
            {
                if (Mathf.Abs(rb.velocity.x) < .1f)
                {
                    state = State.cannonIdle;
                }
            }


            else if (Mathf.Abs(rb.velocity.x) > 2f)
            {
                //Moving
                state = State.cannonRunning;
            }

            else
            {
                state = State.cannonIdle;
            }
        }

    }
    
}
