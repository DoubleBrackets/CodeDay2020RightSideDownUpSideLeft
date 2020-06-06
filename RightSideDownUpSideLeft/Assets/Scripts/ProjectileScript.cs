using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Vector2 localVel;
    private void Start()
    {
        EntityManager.entityManager.entityFixedUpdate += KeepVelLocal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 || collision.gameObject.layer == 8)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                PlayerEntityScript.playerEntityScript.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }

    //Converts intended local velocity into global, in order to keep velocity through rotations
    void KeepVelLocal()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(localVel);
    }

    private void OnDestroy()
    {
        EntityManager.entityManager.entityFixedUpdate -= KeepVelLocal;
    }
}
