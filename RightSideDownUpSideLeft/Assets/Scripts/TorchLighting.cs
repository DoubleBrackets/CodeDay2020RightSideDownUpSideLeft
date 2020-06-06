using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchLighting : MonoBehaviour
{
    Light2D lighting;
    public float range = 1;

    private float baseOuterRadius;
    private float baseInnerRadius;

    private float flickerInterval = 0.15f;
    private float flickerTimer = 0f;
    private void Awake()
    {
        lighting = gameObject.GetComponent<Light2D>();

        baseOuterRadius = lighting.pointLightOuterRadius;
        baseInnerRadius = lighting.pointLightInnerRadius;
        EntityManager.entityManager.entityUpdate += FlickerEffect;
    }

    void FlickerEffect()
    {
        if(flickerTimer >= 0)
        {
            flickerTimer -= Time.deltaTime;
            if(flickerTimer < 0)
            {
                flickerTimer = flickerInterval;
                lighting.pointLightOuterRadius = baseOuterRadius + Random.Range(-range, range);
                lighting.pointLightInnerRadius = baseInnerRadius + Random.Range(-range, range);
            }
        }
    }


}
