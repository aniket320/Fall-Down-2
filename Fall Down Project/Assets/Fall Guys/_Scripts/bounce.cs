using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class bounce : MonoBehaviour
{
    Vector3 backwardDir;
    [SerializeField] private float backwardForce = 3f;
    [SerializeField] private float UpwardForce = 2f;
    [SerializeField] private float stunnedtime = .5f;
    void OnCollisionEnter(Collision other)
    {
        foreach(ContactPoint c in other.contacts)
        {
            if (other.gameObject.CompareTag("Player") )
            {
               Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                backwardDir = c.normal;
                StartCoroutine(PlayerStunned());
                rb.velocity = ( -backwardDir * backwardForce ) +(Vector3.up * UpwardForce);                
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                backwardDir = c.normal;
                StartCoroutine(EnemyStunned());
                rb.velocity = (-backwardDir * backwardForce) + (Vector3.up * UpwardForce);
            }
        }         
     }
    IEnumerator PlayerStunned()
    {
        Playercontroller.instance.canMove = false;
        yield return new WaitForSeconds(stunnedtime);
        Playercontroller.instance.canMove = true;
    }
    IEnumerator EnemyStunned()
    {
        EnemyAI.instance.canMove = false;
        yield return new WaitForSeconds(stunnedtime);
        EnemyAI.instance.canMove = true;
    }

}
