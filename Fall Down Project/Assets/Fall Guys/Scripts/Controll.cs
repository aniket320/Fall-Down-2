using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    public static Controll instance;
        
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("thisinstance");
        }
        else
        {
            Debug.Log("destroy");
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }


}
