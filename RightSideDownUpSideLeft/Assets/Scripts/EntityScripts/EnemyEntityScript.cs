using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyEntityScript : EntityScript
{
    public float aggroDistance;

    public bool hasKey = false;


    protected LayerMask groundedMask;
    private void Awake()
    {
        groundedMask = LayerMask.GetMask("Terrain", "NPCBorder", "NPCJumpBorder");
    }

    protected bool CheckForTarget()
    {
        Vector2 pos = PlayerEntityScript.playerEntityScript.gameObject.transform.position;
        Vector2 diff = pos - (Vector2)transform.position;
        if (diff.magnitude > aggroDistance)
            return false;
        //Checks for walls
        RaycastHit2D hit = Physics2D.Raycast(transform.position, diff, diff.magnitude, groundedMask);
        if(hit.collider == null)
        {
            return true;
        }
        return false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EntityScript>().TakeDamage(1);
        }
    }
}
