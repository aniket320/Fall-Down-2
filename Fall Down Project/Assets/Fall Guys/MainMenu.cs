using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject isQuitPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] Text percentageText;
   // [SerializeField]public AudioMixer audiomixer;
    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Play()
    {
        Debug.Log("Play");
    }   
   
    public void QuitYes()
    {
        Application.Quit();
        Debug.Log("isquit");
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
