using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UserNameMenu; public GameObject usernamePanel { get { return UserNameMenu; } set { UserNameMenu = value; } }
    [SerializeField] private GameObject connetPanel;
    [SerializeField] private GameObject disconnect;
    [SerializeField] private GameObject loding;
    [SerializeField] private TMP_InputField UserNameInputField; 
    [SerializeField] private TMP_InputField JoinGameInputField;
    [SerializeField] private TMP_InputField CreateGameInputField;
    [SerializeField] private GameObject StartButton;
    [SerializeField] public int levelGenerate;
    private void Awake()
    {
        //PhotonNetwork.ConnectUsingSettings(VersionName);
    }
    private void Start()
    {       
        if(instance == null)
        {
            instance = this;
        }
        loding.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    public void connectbutton()
    {
        UserNameMenu.SetActive(false);       
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        loding.gameObject.SetActive(false);
        Debug.Log("isconneted");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnect.gameObject.SetActive(true);
    }
    public override void OnJoinedLobby()
    {
        if (disconnect.activeSelf)
            disconnect.SetActive(false);

    }
    public void UserNameInput()
    {
        if (UserNameInputField.text.Length >= 4)
        {
            StartButton.SetActive(true);
        }
        else
            StartButton.SetActive(false);
    }

    public void startButtonClick()
    {
        UserNameMenu.SetActive(false);
        //PhotonNetwork.playerName = UserNameInputField.text;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateGameInputField.text, new RoomOptions() { MaxPlayers = 5 }, null);
    }

    public void JoinRoom()
    {
        //RoomOptions roomOptions = new RoomOptions();
        //roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinRoom(JoinGameInputField.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("isjoindromm");
        levelGenerate = Random.Range(1, 2);
        PhotonNetwork.LoadLevel(levelGenerate);
        
      
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("room isn't found");
    }
}
