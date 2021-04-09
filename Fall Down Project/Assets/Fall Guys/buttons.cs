using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{


    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void CreateLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Backbutton()
    {
        SceneManager.LoadScene("Menu");
    }
}
