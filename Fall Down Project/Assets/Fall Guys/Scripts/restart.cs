using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnpoint;





    private void OnCollisionEnter(Collision g)
    {
        if(g.gameObject.CompareTag("Player"))
        {
            Player.transform.position = respawnpoint.transform.position;
        }
    }
}
