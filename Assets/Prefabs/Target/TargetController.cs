using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public ExplosionController explosionController;

    void Awake()
    {
        explosionController.OnStopped += () =>
        {
            Debug.Log("OnStopped");
            Destroy(gameObject);
        };
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            explosionController.Play();
        }
    }
}


