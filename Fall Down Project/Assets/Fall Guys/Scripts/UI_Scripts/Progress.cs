using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Progress : MonoBehaviour
{
    
    int progress = 0;


    public Slider slider;

    void Awake()
    {
        if (PlayerPrefs.HasKey("progress"))
        {

            Load();
        }
    }
public void UpdateProgress()
    {
        progress++;
        slider.value = progress;
       Save();
    }


   private void Load()
    {
        slider.value = PlayerPrefs.GetFloat("progress");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("progress", slider.value);

    }
    /*
        public Text obj_text;
        public InputField display;

        // Start is called before the first frame update
        void Start()
        {
            obj_text.text = PlayerPrefs.GetString("username");
        }

        public void Create()
        {
            obj_text.text = display.text;
            PlayerPrefs.SetString("username", obj_text.text);
            PlayerPrefs.Save();
        }

        // Update is called once per frame
        void Update()
        {

        }*/
}
