using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class audio : MonoBehaviour
{
   Text percentageText;
    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text>();
    }

    public void textvol(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
