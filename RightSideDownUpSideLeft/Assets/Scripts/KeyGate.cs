using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGate : MonoBehaviour
{
    //Stick this in an object that requires a key to open

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerInteractScript playerInteractScript = PlayerInteractScript.playerInteractScript;
            if (playerInteractScript.hasKey)
            {
                playerInteractScript.UseKey(gameObject);
                Invoke("KeyUsed", 1.5f);
            }
        }
    }

    void KeyUsed()
    {
        Destroy(gameObject);
    }
}
