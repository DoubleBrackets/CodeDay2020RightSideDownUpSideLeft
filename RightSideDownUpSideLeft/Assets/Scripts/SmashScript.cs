using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashScript : MonoBehaviour
{
    //Components
    Rigidbody2D playerRigidbody;
    Collider2D playerCollider;

    public float smashThreshhold;
    public float smashRadius;

    bool isInSmash = false;

    private float invulnPeriod = 0.2f;
    private float invulnTimer = 0f;

    void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
        //Invuln timer
        if(invulnTimer > 0)
        {
            invulnTimer -= Time.deltaTime;
            if(invulnTimer <= 0)
            {
                PlayerEntityScript.playerEntityScript.isInvulnerable = false;

                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                RotationScript.rotationScript.DecrementCanRotate();
            }
        }

        Vector2 vel = playerRigidbody.velocity;

        //If enough velocity to pass threshhold and enter smash
        if(vel.y <= -smashThreshhold)
        {
            if(!isInSmash)
            {
                PlayerEntityScript.playerEntityScript.isInvulnerable = true;
                isInSmash = true;
                ParticleManager.particleManager.PlayParticle("SmashParticle");
            }
        }
        else//Exit smash on landing or velocity loss
        {
            if(isInSmash)
            {

                DealSmashDamage();
                isInSmash = false;
                ParticleManager.particleManager.PlayParticle("SmashImpactParticle");
                ParticleManager.particleManager.StopParticle("SmashParticle");

                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                RotationScript.rotationScript.IncrementCanRotate();
                invulnTimer = invulnPeriod;
            }
        }
    }

    void DealSmashDamage()
    {
        //Source of AOE is bottom of players hitbox
        Vector2 source = (Vector2)playerCollider.bounds.center + playerCollider.bounds.extents.y * Vector2.down;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(source, smashRadius, Vector2.zero, 0);
        foreach(RaycastHit2D hitObject in hit)
        {
            if(hitObject.collider.gameObject.CompareTag("Enemy"))
            {
                hitObject.collider.gameObject.GetComponent<EntityScript>().TakeDamage(1);
            }
        }
    }
}
