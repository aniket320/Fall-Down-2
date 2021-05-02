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
    //[SerializeField] private GameObject MainMenu;
    public GameObject WinPanel;
    public TMP_Text WinnernameText;
    //[SerializeField] private int NoOfPlayers;
    //public int NoOfPlayerCanQualifie;
    //public int NoOfPlayerQualified;
    public GameObject instatiatepos;
    public bool firstPlayer;
    public TMP_Text GameStartComeDowntext;
    public int GameStartComeDown;
    private float starttime;
    bool comeDownStart = true;
    public bool comeDownOver = false;
    //public GameObject[] QualifiedPlayer;

    private void Awake()
    {
        
    }
    private void Start()
    {
        
        starttime = GameStartComeDown;
        comeDownOver = false;
        if (instace == null)
        {
            instace = this;
        }
        for (int i = PhotonNetwork.PlayerList.Count(); i <= 20; i++)
        {
            PhotonNetwork.Instantiate(EnemyPrefab.name, instatiatepos.transform.position, Quaternion.identity, 0);
        }

        instatiatepos = GameObject.FindGameObjectWithTag("RespawnPos");
        PhotonNetwork.Instantiate(PlayerPrefab.name, instatiatepos.transform.position, Quaternion.identity,0);
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
        if (comeDownStart)
        {
            starttime -= Time.deltaTime;
            GameStartComeDown = Mathf.RoundToInt(starttime);
            GameStartComeDowntext.text = GameStartComeDown.ToString();
            if (GameStartComeDown <= 0)
            {
                GameStartComeDowntext.text = "Go!";
                comeDownStart = false;
                comeDownOver = true;
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
        GameStartComeDowntext.gameObject.SetActive(false);

    }
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
        yield return new WaitForSeconds(2);
    }
    //public IEnumerator NextLevel()
    //{
    //    yield return new WaitForSeconds(4);
    //    LobbyManager.instance.Play();    
    //}
}
