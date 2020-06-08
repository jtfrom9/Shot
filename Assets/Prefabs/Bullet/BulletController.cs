using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rigidbody;
    ParticleSystem particleSystem;
    public event Action OnHit;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        // Shot(new Vector3(0, 500, 2000));
    }

    public void Shot(Vector3 dir)
    {
        rigidbody.AddForce(dir);
    }

    void OnCollisionEnter(Collision other)
    {
        rigidbody.isKinematic = true;
        particleSystem.Play();
        if (other.gameObject.tag == "Target")
        {
            OnHit?.Invoke();
            OnHit = null;
        } else {
            Debug.Log($"Collition with {other.gameObject.name}");
        }
    }

    void Update()
    {
        if (transform.position.y < -100f)
        {
            Destroy(gameObject);
        }
    }
}

