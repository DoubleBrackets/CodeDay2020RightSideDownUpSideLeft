using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public static PlayerMovementScript playerMovementScript;
    //Components
    public Rigidbody2D playerRigidBody;
    public BoxCollider2D playerHitbox;
    public Animator animator;

    //Input vars
    private int horizontalInput;

    //prevgrounded
    private bool prevGrounded;
    //renderer
    public SpriteRenderer render;
    //Movement vars
    public float moveSpeed;
    public float movementForce = 200f;

    public float jumpVel;
    private float jumpCooldown = 0.2f;
    private float jumpCooldownTimer = 0f;

    //States
    private int movementActive = 0;
    private int slowdownActive = 0;

    private bool isGrounded;

    //Masks
    public LayerMask groundedmask;



    //Movement particles
    private float movementParticleCooldown = 0.16f;
    private float movementParticleTimer = 0f;



    private void Awake()
    {
        playerMovementScript = this;
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        isGrounded = GetIsGrounded();
        if (isGrounded == false)
            animator.SetBool("isFall", true);
            prevGrounded = false;

        //Timers
        if (jumpCooldown >= 0)
            jumpCooldownTimer -= Time.deltaTime;
        if(movementParticleTimer > 0 )
        {
            movementParticleTimer -= Time.deltaTime;
        }

        horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        //Jumping
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
            Jump();
        }

        if (prevGrounded == false && isGrounded == true)
        {
            animator.SetBool("isJump", false);
        }
        if (isGrounded == true)
            animator.SetBool("isFall", false);
 
        //filps sprite render and collider
        if (horizontalInput == -1)
        {
            render.flipX = false;

        }
        if (horizontalInput ==   1)
        {
            render.flipX = true;
        }

        //Walking particles
        if(movementParticleTimer <= 0 && isGrounded && horizontalInput != 0)
        {
            movementParticleTimer = movementParticleCooldown;
            ParticleManager.particleManager.PlayParticle("WalkParticle");
        }
       
    }

    public void FixedUpdate()
    {
        //Horizontal movement
        if(movementActive == 0)
        {
            float xVelAfterForce = playerRigidBody.velocity.x + (movementForce * horizontalInput / playerRigidBody.mass) * Time.fixedDeltaTime;

            if(Mathf.Abs(xVelAfterForce) <= moveSpeed)//If force does not go over movespeed, apply force 
            {
                playerRigidBody.velocity = new Vector2(xVelAfterForce,playerRigidBody.velocity.y);
                animator.SetBool("isWalk", false);
      
            }
            else//Otherwise directly set velocity
            {
                if (horizontalInput != 0)
                    SetXVelocity(horizontalInput * moveSpeed);
                    animator.SetBool("isWalk", true);

            }
        }
        //Slowdown
        bool _slowDownActive = (movementActive > 0);
        if(slowdownActive == 0)
        {
            if (horizontalInput == 0 || _slowDownActive)
                SetXVelocity(playerRigidBody.velocity.x * 0.8f);
        }

        if (horizontalInput == 1)
        {
            
        }

    }

  

    void Jump()
    {
        
        if (jumpCooldownTimer <= 0 && isGrounded)//Checks preconditions of jumping
        {
            prevGrounded = true;
            jumpCooldownTimer = jumpCooldown;
            animator.SetBool("isJump", true);
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpVel);
        }
            
    
    }
    private bool GetIsGrounded()
    {
        RaycastHit2D groundedHit = Physics2D.BoxCast(playerHitbox.bounds.center, playerHitbox.bounds.size, 0, Vector2.down, 0.2f, groundedmask);
        if(groundedHit.collider != null)
        {
            return true;
            
        }
        return false;
    }

    public Vector2 GetMouseVector()
    {
        Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
        return diff;
    }

    public void SetXVelocity(float xVel)
    {
        playerRigidBody.velocity = new Vector2(xVel, playerRigidBody.velocity.y);
    }
    public void SetYVelocity(float yVel)
    {
        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x,yVel);
    }

    public void IncrementMovementActive()
    {
        movementActive++;
    }

    public void DecrementMovementActive()
    {
        movementActive--;
    }

    public void IncrementSlowdownActive()
    {
        slowdownActive++;
    }

    public void DecrementSlowdownActive()
    {
        slowdownActive--;
    }



}
