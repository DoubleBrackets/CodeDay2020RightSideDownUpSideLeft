using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractScript : MonoBehaviour
{
    public static PlayerInteractScript playerInteractScript;

    public bool hasKey = false;

    private void Awake()
    {
        playerInteractScript = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            InteractButtonEvent?.Invoke();
        }
    }

    public event Action InteractButtonEvent;


    public void GiveKey()
    {
        if (hasKey)
            return;
        KeyVisualScript.keyVisualScript.GetKeyVisuals();
        hasKey = true;
    }


    public void UseKey(GameObject pos)
    {
        if (!hasKey)
            return;
        hasKey = false;
        StartCoroutine(KeyVisualScript.keyVisualScript.UseKeyVisuals(pos));
    }
}
