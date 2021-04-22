using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{

    public float str = 0.2f;
    private Rigidbody rb;


     void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="Bouncer")
        {

            rb.AddForce(rb.velocity*str,ForceMode.Impulse);
            //Vector3 direction = (transform.position - other.transform.position).normalized;
           // rb.velocity = Vector3.forward * speed;
        }
    }
}
