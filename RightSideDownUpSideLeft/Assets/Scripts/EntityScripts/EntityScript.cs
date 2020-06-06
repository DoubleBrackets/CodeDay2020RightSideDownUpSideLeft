using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityScript : MonoBehaviour
{
    public int maxHealth;
    protected int health;
    protected void CreateEntity()
    {
        health = maxHealth;
    }

    protected abstract void OnEntityDeath();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnEntityDeath();
        }
    }

}
