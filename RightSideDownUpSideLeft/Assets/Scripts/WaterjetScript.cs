using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterjetScript : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        if (other.CompareTag("FirejetHazard"))
        {
            int i = 0;
            while (i < numCollisionEvents)
            {
                other.GetComponent<FirejetHazardScript>().DisableFire(collisionEvents[i].intersection);
                break;
            }
        }
    }
}
