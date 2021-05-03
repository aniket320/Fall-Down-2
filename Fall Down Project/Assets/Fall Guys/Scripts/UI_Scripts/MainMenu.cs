using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject isQuitPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private TMP_InputField UserName;
    [SerializeField] private TMP_Text UsernamesDisplay;
    public AudioMixer audiomixer;
        // [SerializeField]public AudioMixer audiomixer;
    // Start is called before the first frame update
    void Start()
    {


    }


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
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


   

}
