using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Sentence
{
    [SerializeField] public string[] word;
}

[Serializable]
public class Selection
{
    [SerializeField] public string characterSentence;
    [SerializeField] public Sentence[] type = new Sentence[3];
}

[CreateAssetMenu(fileName = "DialogueContainer", menuName = "Scriptable Objects/DialogueContainer")]
public class DialogueContainer : ScriptableObject
{
    [SerializeField] public Selection[] grouping;
}
