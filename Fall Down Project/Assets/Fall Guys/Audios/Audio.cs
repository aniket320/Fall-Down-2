using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField]private  Slider Volslider;

    public AudioMixer Am;


    //private void SetValue(float slidervalue)
   // {
   //     Am.SetFloat("music", Mathf.Log10(slidervalue));
  //  }
    // Start is called before the first frame update

    void Awake()
    {
        if (PlayerPrefs.HasKey("music"))
        {
           
            Load();
        }
        

    }

    // Update is called once per frame


    public void changevol()
    {
        AudioListener.volume = Volslider.value;
        Save();
    }


    private void Load()
    {
        Volslider.value = PlayerPrefs.GetFloat("music");
    } 
    private void Save()
    {
        PlayerPrefs.SetFloat("music", Volslider.value);    
       
        
    }
}
