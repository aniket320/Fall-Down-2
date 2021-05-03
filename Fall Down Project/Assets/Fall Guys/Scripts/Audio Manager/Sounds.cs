using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds 
{
    public string name;

    public AudioClip clip;
    
    [Range (0f,1f)]
    public float Volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
    


  
}
