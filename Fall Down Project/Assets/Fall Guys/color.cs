using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Material player;

    private AudioSource audiosource;
    public AudioClip sound;
    
    // Update is called once per frame
    void Update()
    {
       player.color=fcp.color;
       
     
    }
   public void save()
    {
        PlayerPrefs.Save();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            audiosource = GetComponent<AudioSource>();
            audiosource.clip = sound;
            audiosource.Play();
        }
    }

}
