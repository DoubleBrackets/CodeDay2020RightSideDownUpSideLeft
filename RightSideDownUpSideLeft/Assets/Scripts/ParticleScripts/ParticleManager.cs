﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager particleManager;

    private void Awake()
    {
        particleManager = this;
    }


    //Event system for player particles
    public event Action<String> playParticleEvent;
    public void PlayParticle(string _id)
    {
        playParticleEvent?.Invoke(_id);
    }

    public event Action<String> stopParticleEvent;
    public void StopParticle(string _id)
    {
        stopParticleEvent?.Invoke(_id);
    }

    public event Action<String,bool> setParticleActiveEvent;
    public void SetParticleActive(string _id, bool val)
    {
        if (setParticleActiveEvent != null)
        {
            setParticleActiveEvent(_id, val);
        }
    }

}
