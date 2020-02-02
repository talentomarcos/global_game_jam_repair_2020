using UnityEngine;
using System.Collections;

[System.Serializable]
public class AudioSerial : ScriptableObject
{
    public AudioType _type;
    public AudioClip source;
    [Range(0, 1f)]
    public float _relativeVolume = 1;
    public bool _enabled = true;
}

public enum AudioType
{
    SFX,
    MUSIC
}