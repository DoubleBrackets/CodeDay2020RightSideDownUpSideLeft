using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHazardScript : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        EntityScript entity = collision.gameObject.GetComponent<EntityScript>();
        if (entity != null)
        {
            entity.TakeDamage(1);
        }
    }
}
