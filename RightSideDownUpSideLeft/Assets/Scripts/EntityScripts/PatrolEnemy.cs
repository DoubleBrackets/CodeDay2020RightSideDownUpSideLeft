using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyEntityScript
{
    /*
     * NOTE
     * Any colliders with the layer NPCBorder will cause this enemy to change directions, and prevent it from jumping
     * Likewise, NPCJumpBorder will cause force this enemy to jump 
     * 
     * by default, this enemy will walk off ledges(doesnt check for deadly holes, lmao)
     */




    //Components
    protected Rigidbody2D enemyRigidbody;
    protected Collider2D enemyHitbox;


    private void Start()
    {
        CreateEntity();
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
        enemyHitbox = gameObject.GetComponent<Collider2D>();

        EntityManager.entityManager.entityFixedUpdate += Patrol;

        currentDir = 1;
    }


    public float moveSpeed;
    public float jumpHeight;
    public float jumpVel;

    private float movementForce = 100f;

    public bool canJump;

    private int currentDir;



    void Patrol()
    {
        gameObject.transform.rotation = Quaternion.identity;

        bool isGrounded = (CheckForLeftGrounded() || CheckForRightGrounded());
        if(isGrounded)//Can only change direction on ground
        {
            if (CheckForObstacle(currentDir))
            {
                if (CheckForJump(currentDir) && canJump)//If able to jump over obstacle
                {
                    Jump();
                }
                else//Otherwise turn around
                {
                    currentDir *= -1;
                }
            }
        }

        //Applies horizontal movement
        float xVelAfterForce = enemyRigidbody.velocity.x + (movementForce * currentDir / enemyRigidbody.mass) * Time.fixedDeltaTime;

        if (Mathf.Abs(xVelAfterForce) <= moveSpeed)//If force does not go over movespeed, apply force 
        {
            enemyRigidbody.velocity = new Vector2(xVelAfterForce, enemyRigidbody.velocity.y);
        }
        else//Otherwise directly set velocity
        {
            enemyRigidbody.velocity = new Vector2(currentDir * moveSpeed, enemyRigidbody.velocity.y);
        }

    }

    protected void Jump()
    {
        enemyRigidbody.velocity = new Vector2(enemyRigidbody.velocity.x, jumpVel);
    }

    //Checks for any opening ledges the size of the hitbox that this enemy can jump onto/through
    protected bool CheckForJump(int dir)
    {
        for(float x = 0.1f;x < jumpHeight;x+=0.5f)
        {
            Vector2 startPos = (Vector2)enemyHitbox.bounds.center + new Vector2(dir * 0.1f, x);
            RaycastHit2D groundedHit = Physics2D.BoxCast(startPos, enemyHitbox.bounds.size, 0, dir * Vector2.right, 0.5f, groundedMask);
            if (groundedHit.collider == null || groundedHit.collider.gameObject.layer == 12)
            {
                return true;
            }
            else if(groundedHit.collider.gameObject.layer == 11)
            {
                return false;
            }
        }
        return false;
    }

    //Checks for any terrain or NPCBorders to close right/left
    protected bool CheckForObstacle(int dir)
    {
        Vector2 size = new Vector2(enemyHitbox.bounds.size.x, enemyHitbox.bounds.size.y - 0.2f);
        RaycastHit2D groundedHit = Physics2D.BoxCast(enemyHitbox.bounds.center, size,0,dir * Vector2.right, 0.5f, groundedMask);
        if (groundedHit.collider != null)
        {
            return true;
        }
        return false;
    }

    //Checks if grounded on right edge of hitbox
    protected bool CheckForRightGrounded()
    {
        Vector2 startPoint = (Vector2)enemyHitbox.bounds.center + new Vector2(enemyHitbox.bounds.extents.x-0.1f, - enemyHitbox.bounds.extents.y);
        RaycastHit2D groundedHit = Physics2D.Raycast(startPoint, Vector2.down, 0.2f, groundedMask);
        if(groundedHit.collider != null)
        {
            return true;
        }
        return false;
    }
    //Checks if grounded on left edge of hitbox
    protected bool CheckForLeftGrounded()
    {
        Vector2 startPoint = (Vector2)enemyHitbox.bounds.center + new Vector2(-enemyHitbox.bounds.extents.x+0.1f, -enemyHitbox.bounds.extents.y);
        RaycastHit2D groundedHit = Physics2D.Raycast(startPoint, Vector2.down, 0.2f, groundedMask);
        if (groundedHit.collider != null)
        {
            return true;
        }
        return false;
    }

    protected override void OnEntityDeath()
    {
        if(hasKey)
        {
            PlayerInteractScript.playerInteractScript.GiveKey();
        }
        EntityManager.entityManager.entityFixedUpdate -= Patrol;
        Destroy(this.gameObject);
    }

}
