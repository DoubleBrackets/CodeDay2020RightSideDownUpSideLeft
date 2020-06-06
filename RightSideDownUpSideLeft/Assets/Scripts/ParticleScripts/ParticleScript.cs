using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string particleId;

    public bool useGameobjectId = false;

    ParticleSystem pSys;

    void Start()
    {
        pSys = gameObject.GetComponent<ParticleSystem>();
        ParticleManager.particleManager.setParticleActiveEvent += SetActive;
        ParticleManager.particleManager.playParticleEvent += Play;
        ParticleManager.particleManager.stopParticleEvent += Stop;
        if(useGameobjectId)
        {
            particleId = "" + gameObject.GetInstanceID();
        }
    }


    void SetActive(string _id, bool val)
    {
        if(_id.CompareTo(particleId) == 0)
        {
            if(val)
            {
                var c = pSys.main;
                c.maxParticles = 1000;
            }
            else
            {
                var c = pSys.main;
                c.maxParticles = 0;
            }
        }
    }

    void Play(string _id)
    {
        if (_id.CompareTo(particleId) == 0)
        {
            pSys.Play();
            var em = pSys.emission;
            em.enabled = true;
        }
    }

    void Stop(string _id)
    {
        if (_id.CompareTo(particleId) == 0)
        {
            var em = pSys.emission;
            em.enabled = false;
        }
    }

    private void OnDestroy()
    {
        ParticleManager.particleManager.setParticleActiveEvent -= SetActive;
        ParticleManager.particleManager.playParticleEvent -= Play;
        ParticleManager.particleManager.stopParticleEvent -= Stop;
    }
}
