using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respwaun : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 respawnpoint;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnpoint = transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {

            transform.position = respawnpoint;
        }

        if (other.tag == "spawn")
        {
            respawnpoint = other.transform.position;
        }


    }

}
