using UnityEngine;
using System.Collections.Generic;
using System;

public enum ESoundType : Int16
{
    KEYBOARD,
    GOODWORD,
    BADLETTER
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource[] audioSources;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void SoundEffect(ESoundType type)
    {
        audioSources[(Int16)type].Play();
    }
}
