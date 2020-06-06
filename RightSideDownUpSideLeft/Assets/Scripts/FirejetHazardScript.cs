using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirejetHazardScript : MonoBehaviour
{
    public float disableTime;
    private float disableTimer = 0f;

    ParticleSystem pSys;

    public GameObject blockCollider;

    private int particleHitCount = 0;

    private void Start()
    {
        pSys = gameObject.GetComponent<ParticleSystem>();
        EntityManager.entityManager.entityUpdate += FirejetUpdate;
    }

    void OnParticleCollision(GameObject other)
    {
        EntityScript entity = other.GetComponent<EntityScript>();
        if(entity != null)
        {
            particleHitCount += 1;
            if(particleHitCount >= 5)
            {
                entity.TakeDamage(1);
                particleHitCount = 0;
            }
        }
    }

    void FirejetUpdate()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0)
            {
                particleHitCount = 0;
                blockCollider.SetActive(false);
            }
        }
    }


    public void DisableFire(Vector2 pos)
    {
        blockCollider.SetActive(true);
        blockCollider.transform.position = pos;
        disableTimer = disableTime;
    }

}
