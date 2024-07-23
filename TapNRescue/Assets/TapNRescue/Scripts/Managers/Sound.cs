using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;
    public float pitch = 1;
    public bool loop;
    public bool playOnAwake;
    [Range(0, 1)]public float volume;
    [Range(0, 1)] public float spatialBlend;

    [HideInInspector]
    public AudioSource source;
}
