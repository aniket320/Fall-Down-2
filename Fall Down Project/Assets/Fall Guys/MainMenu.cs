using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        LobbyManager.instance.OnConnectedToMaster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
    }
    public void Party()
    {
        mainmenu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
