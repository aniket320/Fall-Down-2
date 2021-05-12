using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinishLine : MonoBehaviourPun
{
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject[] Players;
    private void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        Players = GameObject.FindGameObjectsWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") )
        {
           
            GameManager.instace.WinPanel.SetActive(true);
            GameManager.instace.WinnernameText.text = " Winner: " + other.gameObject.GetComponent<Playercontroller>().PlayerUsername;
            if (Players != null)
            {
                foreach (GameObject p in Players)
                {
                    p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
            }
            if (enemy != null)
            {
                foreach (GameObject e in enemy)
                {
                    e.GetComponent<NavMeshAgent>().isStopped = true;
                }
            }
               

        }
        if (other.CompareTag("Enemy"))
        {
            GameManager.instace.WinPanel.SetActive(true);
            GameManager.instace.WinnernameText.text = " Winner: " + other.gameObject.GetComponent<EnemyAI>().EnemyNameText.text;
            if (enemy != null)
            {
                foreach (GameObject e in enemy)
                {
                    e.GetComponent<NavMeshAgent>().isStopped = true;
                }
            }
            if (Players != null)
            {
                foreach (GameObject p in Players)
                {
                    p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
            }
         
        }

    }
}
