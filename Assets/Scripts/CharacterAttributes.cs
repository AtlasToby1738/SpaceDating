using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Scriptable Objects/CharacterAttributes")]
public class CharacterAttributes : ScriptableObject
{
    [SerializeField] public Sprite[] sprites = new Sprite[3];
    [SerializeField] public AudioClip[] sounds = new AudioClip[3];
    [SerializeField] public string name;
    [SerializeField] public int score;
}
