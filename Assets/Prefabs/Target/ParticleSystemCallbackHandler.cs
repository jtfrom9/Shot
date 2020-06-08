using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemCallbackHandler : MonoBehaviour
{
    public event Action OnStopped;
    void Awake()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        Debug.Log($"OnParticleSystemStopped: {gameObject.name}");
        OnStopped?.Invoke();
    }

    void OnDestroy()
    {
        OnStopped = null;
    }
}
