using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public event Action OnStopped;
    void onStopped()
    {
        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            if (!ps.isStopped)
            {
                Debug.Log("not stopped");
                return;
            }
        }
        OnStopped?.Invoke();
    }

    void Start()
    {
        foreach(var h in GetComponentsInChildren<ParticleSystemCallbackHandler>()) {
            h.OnStopped += onStopped;
        }
    }

    public void Play()
    {
        foreach (var ps in GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
    }
}
