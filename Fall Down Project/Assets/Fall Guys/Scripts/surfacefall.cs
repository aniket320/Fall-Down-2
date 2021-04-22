using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surfacefall : MonoBehaviour
{

    Rigidbody rb;


     void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {

            
           rb.isKinematic=false;
            Destroy(gameObject, 1f);
        }
    }
}
