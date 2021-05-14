using Photon.Pun;
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
    int firstRun;
    public TMP_Dropdown DD;    
    public RenderPipelineAsset[] qualitylevels;
   
    // Start is called before the first frame update
    void Start()
    {
        firstRun = 0;
        firstRun = PlayerPrefs.GetInt("saveUserNameForFirsttime");

        if (firstRun != 1)
        {
            PlayerPrefs.SetString("UserName", PhotonNetwork.NickName = "FallDown#" + Random.Range(0000, 9999));
            PlayerPrefs.SetInt("saveUserNameForFirsttime", 1);
            firstRun = 1;
        }

        AudioManager.instance.audioStop = true;
        DD.value = QualitySettings.GetQualityLevel();
        loding.gameObject.SetActive(true);
       
               
        
        if (PlayerPrefs.GetInt("ShowIntersticialAds") == 1)
        {
            AdManager.Instance.ShowAds();
            PlayerPrefs.SetInt("ShowIntersticialAds", 0);
        }
            //ads
        else
            return;

       
       
      
      
        
    }
       
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

    
    public void saveUsername()
    {        
        PlayerPrefs.SetString("UserName", PhotonNetwork.NickName = UserName.text);
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("menu",volume);
    }

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
