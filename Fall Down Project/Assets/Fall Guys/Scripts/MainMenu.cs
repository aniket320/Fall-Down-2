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
    [SerializeField] private Text percentageText;
    [SerializeField] private TMP_InputField UserName;
    [SerializeField] private TMP_Text UsernamesDisplay;
    // [SerializeField]public AudioMixer audiomixer;
    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text>();
        //UserName.text = PhotonNetwork.NickName = "FallDown#" + Random.Range(0, 1000).ToString("0000");
        //PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void LateUpdate()
    {
        UsernamesDisplay.text = PlayerPrefs.GetString("UserName");

    }
    public void Play()
    {
        Debug.Log("Play");
    }   
    public void Party()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        PlayerPrefs.SetString("UserName",PhotonNetwork.NickName = UserName.text);
    }


    /*public float SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }*/

    public void SetQaulity(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void textvol(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
