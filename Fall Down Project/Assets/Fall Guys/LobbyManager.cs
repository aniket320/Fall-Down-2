using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;
    //[SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UserNameMenu; public GameObject usernamePanel { get { return UserNameMenu; } set { UserNameMenu = value; } }
    [SerializeField] private GameObject disconnect;
    [SerializeField] private GameObject loding;
    [SerializeField] private TMP_InputField UserNameInputField;
    [SerializeField] private TMP_InputField JoinGameInputField;
    [SerializeField] private TMP_InputField CreateGameInputField;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private TextMeshProUGUI RoomNo;
    [SerializeField] private GameObject connectingPanel;
    [SerializeField] private GameObject inRoom;
    [SerializeField] private Transform PlayerListCount;
    [SerializeField] private GameObject PlayerListPrefab;
    [SerializeField] private GameObject Playbtn;
    private void Awake()
    {
        //PhotonNetwork.ConnectUsingSettings(VersionName);
    }
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        loding.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    //public void connectbutton()
    //{
    //    UserNameMenu.SetActive(false);
    //}
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        loding.gameObject.SetActive(false);
        Debug.Log("isconneted");
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnect.gameObject.SetActive(true);
    }
    public override void OnJoinedLobby()
    {
        if (disconnect.activeSelf)
            disconnect.SetActive(false);
        PhotonNetwork.NickName = "FallDown " + Random.Range(0, 1000).ToString("0000");
    }
    //public void UserNameInput()
    //{
    //    if (UserNameInputField.text.Length >= 4)
    //    {
    //        StartButton.SetActive(true);
    //    }
    //    else
    //        StartButton.SetActive(false);
    //}

    //public void startButtonClick()
    //{
    //    UserNameMenu.SetActive(false);
    //}

    public void CreateRoom()
    {
       
        connectingPanel.SetActive(true);
        float roomNo = Random.Range(60000, 99999);
        PhotonNetwork.CreateRoom(roomNo.ToString(), new RoomOptions() { MaxPlayers = 5 }, null);
        RoomNo.text = "Room Code:" + roomNo;
    }
    public void JoinRoom()
    {
        //RoomOptions roomOptions = new RoomOptions();
        //roomOptions.MaxPlayers = 5;
       
        RoomNo.text = "Room Code:" + JoinGameInputField.text;
        Debug.Log("isJoinedRoom");
        PhotonNetwork.JoinRoom(JoinGameInputField.text);
    }
    public override void OnJoinedRoom()
    {
        connectingPanel.SetActive(false);
        inRoom.SetActive(true);
        Debug.Log("isjoindromm");


        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListPrefab, PlayerListCount).GetComponent<PlayerListCount>().setup(players[i]);
        }
        Playbtn.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Playbtn.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void Play()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("room isn't found");
    }
    public void leaveroom()
    {
        PhotonNetwork.LeaveRoom();
        StartCoroutine(inroomScene());
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListPrefab, PlayerListCount).GetComponent<PlayerListCount>().setup(newPlayer);
    }
    IEnumerator inroomScene()
    {
        yield return new WaitForSeconds(1f);
        inRoom.SetActive(false);
    }

} 
