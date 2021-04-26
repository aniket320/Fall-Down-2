using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Material player;
   

    
    // Update is called once per frame
    void Update()
    {
       player.color=fcp.color;
       
     
    }
   public void save()
    {
        PlayerPrefs.Save();
    }
   
}
