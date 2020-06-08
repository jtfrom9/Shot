using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public ExplosionController explosionController;
    public event Action OnDead;
    Vector3 dir;
    MeshRenderer meshRenderer;
    Collider collider;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
        explosionController.OnStopped += () =>
        {
            Debug.Log("OnStopped");
            Destroy(gameObject);
        };
    }

    void Start()
    {
        iTween.MoveTo(gameObject,
            iTween.Hash("x", Camera.main.transform.position.x,
            "y", Camera.main.transform.position.y,
            "z", Camera.main.transform.position.z,
            "time", UnityEngine.Random.Range(20, 30)));
    }

    void Update()
    {
        if (transform.position.z < Camera.main.transform.position.z + 3.0f &&
            !collider.isTrigger)
        {
            OnDead?.Invoke();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            meshRenderer.material.color = new Color(1.0f, 0.0f, 0.0f, 0.1f);
            collider.isTrigger = true;
            explosionController.Play();
        }
    }
}


