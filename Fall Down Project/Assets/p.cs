using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p : MonoBehaviour
{
    public Material mat;
    public FlexibleColorPicker fcp;

    private void Update()
    {
        mat.color = fcp.color;

    }

    //save code

    private void OnDisable()
    {
        string saveColor = ColorUtility.ToHtmlStringRGB(fcp.color);
        PlayerPrefs.SetString("MyColorValue", saveColor);
    }

    //load code

    private void OnEnable()
    {
        string saveColor = PlayerPrefs.GetString("MyColorValue");
        Color color;
        if (ColorUtility.TryParseHtmlString(saveColor, out color))
            
        
            fcp.color = color;
        
    }


}
