using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class bounce : MonoBehaviour
{

  

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //generate vector in the direction of jump pad's y axis multiplied with a factor jumpForce

    }

    void OnCollisionEnter(Collision other)
     {
         if (other.gameObject.CompareTag("Bouncer"))
         {
            rb.AddForce(Vector3.back * 2, ForceMode.Impulse);
            rb.AddForce(Vector3.up * 3, ForceMode.Impulse);
             
         }
     }

    
    

}
