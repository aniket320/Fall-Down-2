using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject isQuitPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject SettingPanel;
    // Start is called before the first frame update
    void Start()
    {
        //LobbyManager.instance.OnConnectedToMaster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
        //SceneManager.LoadScene("Level1");
    }
    public void Party()
    {
        mainmenu.SetActive(false);
        LobbyManager.instance.usernamePanel.SetActive(true);
    }
    public void Backbutton()
    {
        mainmenu.SetActive(true);
        //SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {       
        isQuitPanel.SetActive(true);
    }
    public void QuitYes()
    {
        Application.Quit();
        Debug.Log("isquit");
    }
    public void QuitNo()
    {
        isQuitPanel.SetActive(false);
    }
    public void help()
    {
        helpPanel.SetActive(true);
    }
    public void Setting()
    {
        SettingPanel.SetActive(true);
    }
}
