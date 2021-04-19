using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instace;
    [SerializeField] private GameObject PlayerPrefab; 
    [SerializeField] private GameObject qualifiedPanel;
    [SerializeField] private GameObject FinishPanel;
    [SerializeField] private int NoOfPlayers;
    public int NoOfPlayerCanQualifie;
    public int NoOfPlayerQualified;
    GameObject instatiatepos;
    public GameObject[] QualifiedPlayer;

    private void Start()
    {

        if (instace == null)
        {
            instace = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);


        instatiatepos = GameObject.Find("InstacePos");
        PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position /*new Vector2(this.transform.position.x * randompos, this.transform.position.y)*/, Quaternion.identity);
        StartCoroutine(LevelStart());


        GameObject[] PlayersCount = GameObject.FindGameObjectsWithTag("Player");
        NoOfPlayers = PlayersCount.Length;
        if (NoOfPlayers % 2 == 0)
        {
            NoOfPlayerCanQualifie = NoOfPlayers / 2;
        }
        else
        {
            NoOfPlayerCanQualifie = (NoOfPlayers + 1) / 2;
        }

        QualifiedPlayer = new GameObject[NoOfPlayerCanQualifie];

    }
    private void Update()
    {
        if(NoOfPlayerQualified == NoOfPlayerCanQualifie)
        {
            FinishPanel.SetActive(true);
            //StartCoroutine(NextLevel());
        }
        //QualifiedPlayer = GameObject.FindGameObjectsWithTag("Player");
    }
    //public void SpawnPlayer()
    //{
    //    //float randompos = Random.Range(-5f, 5f);

    //    //PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position /*new Vector2(this.transform.position.x * randompos, this.transform.position.y)*/, Quaternion.identity);
    //}
    public void coroutineCall()
    {
        StartCoroutine(QualifiedPanel());
    }
    public IEnumerator QualifiedPanel()
    {
        qualifiedPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        qualifiedPanel.SetActive(false);        
    }
    public IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(2);
    }
    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(4);
        LobbyManager.instance.Play();    
    }
}
