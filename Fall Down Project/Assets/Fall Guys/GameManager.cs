using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instace;
    public GameObject PlayerPrefab; 
    public GameObject EnemyPrefab;
    public GameObject[] PlayerInstantiation;
    public GameObject WinPanel;
    public TMP_Text WinnernameText;
    public GameObject instatiatepos;
    public bool firstPlayer;
    public TMP_Text GameStartCountDowntext;
    public int GameStartCountDown;
    private float starttime;
    bool CountDownStart = true;
    public bool CountDownOver = false;
    //[SerializeField] private int NoOfPlayers;
    //public int NoOfPlayerCanQualifie;
    //public int NoOfPlayerQualified;
    //public GameObject[] QualifiedPlayer;


    private void Start()
    {
        AudioManager.instance.Play("IngameAudio");
        PlayerInstantiation = GameObject.FindGameObjectsWithTag("PlayerInstacePos");
        instatiatepos = GameObject.FindGameObjectWithTag("RespawnPos");
        starttime = GameStartCountDown;
        CountDownOver = false;
        if (instace == null)
        {
            instace = this;
        }

        //for (int p = 0; p < PhotonNetwork.PlayerList.Count(); p++)
        //{
        //    PhotonNetwork.Instantiate(PlayerPrefab.name, PlayerInstantiation[p].transform.position, Quaternion.identity, 0);
        //}

        if (PhotonNetwork.PlayerList.Count() == 1)
        {
            for (int i = PhotonNetwork.PlayerList.Count(); i < 10; i++)
            {
                PhotonNetwork.Instantiate(EnemyPrefab.name, PlayerInstantiation[i].transform.position, Quaternion.identity, 0);
            }
            GameObject g = GameObject.FindGameObjectWithTag("PlayerInstacePos");
            PhotonNetwork.Instantiate(PlayerPrefab.name, g.transform.position, Quaternion.identity, 0);
        }
        else
            PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position, Quaternion.identity, 0);



        //}



        StartCoroutine(LevelStart());

        //GameObject[] PlayersCount = GameObject.FindGameObjectsWithTag("Player");
        //NoOfPlayers = PlayersCount.Length;
        //if (NoOfPlayers % 2 == 0)
        //{
        //    NoOfPlayerCanQualifie = NoOfPlayers / 2;
        //}
        //else
        //{
        //    NoOfPlayerCanQualifie = (NoOfPlayers + 1) / 2;
        //}

        //QualifiedPlayer = new GameObject[NoOfPlayerCanQualifie];

    }
    private void Update()
    {
        if (CountDownStart)
        {
            starttime -= Time.deltaTime;
            GameStartCountDown = Mathf.RoundToInt(starttime);
            GameStartCountDowntext.text = GameStartCountDown.ToString();
            if (GameStartCountDown <= 0)
            {
                GameStartCountDowntext.text = "Go!";
                CountDownStart = false;
                CountDownOver = true;
                Invoke("ComeDowntextDisable", 1);
            }
            
        }
       
        //if(NoOfPlayerQualified == NoOfPlayerCanQualifie)
        //{
        //    //FinishPanel.SetActive(true);
        //    //StartCoroutine(NextLevel());
        //}
    }
    private void ComeDowntextDisable()
    {
        GameStartCountDowntext.gameObject.SetActive(false);

    }
   
    public void leaveGame()
    {
        StartCoroutine(ReturnTLobby());
    }
    IEnumerator ReturnTLobby()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene("Party");       
    }
    
    public IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(5);
    }
    //public IEnumerator NextLevel()
    //{
    //    yield return new WaitForSeconds(4);
    //    LobbyManager.instance.Play();    
    //}
    //public void SpawnPlayer()
    //{
    //    //float randompos = Random.Range(-5f, 5f);

    //    //PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position /*new Vector2(this.transform.position.x * randompos, this.transform.position.y)*/, Quaternion.identity);
    //}
    //public void coroutineCall()
    //{
    //    StartCoroutine(QualifiedPanel());
    //}
    //public IEnumerator QualifiedPanel()
    //{
    //    qualifiedPanel.SetActive(true);
    //    yield return new WaitForSeconds(2);
    //    qualifiedPanel.SetActive(false);        
    //}
}
