using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Scriptable Objects/CharacterAttributes")]
public class CharacterAttributes : ScriptableObject
{
    [SerializeField] private Texture2D[] Sprites = new Texture2D[3];

}
