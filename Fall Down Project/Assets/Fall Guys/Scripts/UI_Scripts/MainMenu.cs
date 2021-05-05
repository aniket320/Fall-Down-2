﻿using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public static MainMenu instance;
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject isQuitPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private TMP_InputField UserName;
    [SerializeField] private TMP_Text UsernamesDisplay;
    [SerializeField] private GameObject loding;
    public AudioMixer audiomixer;
    int firstRun = 0;
    public TMP_Dropdown DD;
    
    public RenderPipelineAsset[] qualitylevels;
   // public TMP_Dropdown DD;
    // [SerializeField]public AudioMixer audiomixer;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("MenuAudio");

        //DD.value = QualitySettings.GetQualityLevel();

        loding.gameObject.SetActive(true);
        //PhotonNetwork.ConnectUsingSettings();

        firstRun = PlayerPrefs.GetInt("saveUserNameForFirsttime");

        if (firstRun == 0) // remember "==" for comparing, not "=" which assigns value
        {
            PlayerPrefs.SetString("UserName", PhotonNetwork.NickName = "FallDown#" + Random.Range(0000, 9999));
            PlayerPrefs.SetInt("saveUserNameForFirsttime", 1);
            firstRun = 1;            
        }
        else
        {
            //Do lots of game save loading
            return;
        }


    }
    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinLobby(TypedLobby.Default);
    //    loding.gameObject.SetActive(false);
    //    Debug.Log("isconneted");
    //}
   
        
    private void Awake()
    {

        //PhotonNetwork.AutomaticallySyncScene = true;

    }
    // Update is called once per frame

    void Update()
    {                     
            UsernamesDisplay.text = PlayerPrefs.GetString("UserName");        
    }

    public void Play()
    {                                         
        Debug.Log("Play");
    }

    public void Party()
    {       
        SceneManager.LoadScene("Party");
    }

    public void QuitYes()
    {
        Application.Quit();
        Debug.Log("isquit");
    }

    //public void Username()
    //{
    //    names.text = UserName.text;
    //}

    public void saveUsername()
    {        
        PlayerPrefs.SetString("UserName", PhotonNetwork.NickName = UserName.text);
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("menu",volume);
    }


    // Update is called once per frame





    public void SetQaulity(int qualityIndex)
    {
     QualitySettings.SetQualityLevel(qualityIndex);
       QualitySettings.renderPipeline = qualitylevels[qualityIndex];
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


   

}
