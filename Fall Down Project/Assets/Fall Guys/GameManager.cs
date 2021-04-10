using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private GameObject instatiatepos;
    public static GameManager instace;
    [SerializeField] private GameObject qualifiedPanel;
    private void Start()
    {
        if(instace== null)
        {
            instace = this;
        }
        instatiatepos = GameObject.Find("InstacePos");
        PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position /*new Vector2(this.transform.position.x * randompos, this.transform.position.y)*/, Quaternion.identity); 
    }
    //public void SpawnPlayer()
    //{
    //    //float randompos = Random.Range(-5f, 5f);

    //    //PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position /*new Vector2(this.transform.position.x * randompos, this.transform.position.y)*/, Quaternion.identity);
    //}
    public void timpass()
    {
        StartCoroutine(QualifiedPanel());
    }
    public IEnumerator QualifiedPanel()
    {
        qualifiedPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        qualifiedPanel.SetActive(false);        
    }
}
