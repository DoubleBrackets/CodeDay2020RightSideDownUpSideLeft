using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjectEntity : EntityScript
{
    public bool hasKey = false;

    protected override void OnEntityDeath()
    {
        if(hasKey)
        {
            PlayerInteractScript.playerInteractScript.GiveKey();
        }
        Destroy(gameObject);
    }
}
