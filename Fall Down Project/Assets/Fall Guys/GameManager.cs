using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject instatiatepos;
    private void Start()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position /*new Vector2(this.transform.position.x * randompos, this.transform.position.y)*/, PlayerPrefab.transform.rotation); ;
    }
    //public void SpawnPlayer()
    //{
    //    //float randompos = Random.Range(-5f, 5f);

    //    //PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position /*new Vector2(this.transform.position.x * randompos, this.transform.position.y)*/, Quaternion.identity);
    //}
  
}
