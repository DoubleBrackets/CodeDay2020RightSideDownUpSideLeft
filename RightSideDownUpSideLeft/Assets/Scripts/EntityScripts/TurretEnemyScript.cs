using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyScript : EnemyEntityScript
{
    //Components
    protected Collider2D enemyHitbox;
    protected Rigidbody2D rb;

    public float shootCooldown;
    private float shootTimer = 0;

    public GameObject projectile;

    public float projectileVelocity;

    private float offSet = 1f;

    public GameObject[] patrolPoints;
    private int patrolPointsLength;
    private int currentTarget = 0;

    public float moveSpeed;


    private void Start()
    {
        CreateEntity();
        enemyHitbox = gameObject.GetComponent<Collider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        EntityManager.entityManager.entityFixedUpdate += Turret;

        patrolPointsLength = patrolPoints.Length;

    }

    protected void Turret()
    {
        if(CheckForTarget())
        {
            //Pew
            if (shootTimer <= 0)
            {
                shootTimer = shootCooldown;
                Shoot(PlayerEntityScript.playerEntityScript.gameObject.transform.position);
            }
            else
                shootTimer -= Time.deltaTime;
        }

        if(patrolPointsLength > 1)
        {
            if (currentTarget >= patrolPointsLength)
            {
                currentTarget = 0;
            }

            Vector2 diff = patrolPoints[currentTarget].transform.position - transform.position;
            float time = diff.magnitude / moveSpeed;

            rb.velocity = diff.normalized * moveSpeed;
            if (diff.magnitude <= 0.3f)
            {
                gameObject.transform.position = patrolPoints[currentTarget].transform.position;
                currentTarget++;
            }
        }
    }

    protected void Shoot(Vector2 target)
    {
        Vector2 diff = target - (Vector2)gameObject.transform.position;
        Vector2 startPos = (Vector2)gameObject.transform.position + diff.normalized * offSet;

        //Creates projectile, sets its velocity
        GameObject proj = Instantiate(projectile, startPos, Quaternion.identity, RotationScript.rotationScript.levelContainer.transform);
        Vector2 vel = diff.normalized * projectileVelocity;
        proj.GetComponent<Rigidbody2D>().velocity = vel;
        //Saves the local velocity relative to container
        proj.GetComponent<ProjectileScript>().localVel = vel; //transform.InverseTransformDirection(vel);
    }

    protected override void OnEntityDeath()
    {
        if (hasKey)
        {
            PlayerInteractScript.playerInteractScript.GiveKey();
        }
        EntityManager.entityManager.entityFixedUpdate -= Turret;
        Destroy(this.gameObject);
    }

}
