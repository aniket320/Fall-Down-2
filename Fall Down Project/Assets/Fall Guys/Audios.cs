using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audios : MonoBehaviour
{
    
    private static Audios instance;
  
    void Awake()
    {
       
      if(instance!=null)
        {
            Destroy( gameObject);
           
        }
        else
        {
            instance = this;
           DontDestroyOnLoad(transform.gameObject);
        }

    }
}
