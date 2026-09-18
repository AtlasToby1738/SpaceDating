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
    [SerializeField] public Sentence[] type;
}

[CreateAssetMenu(fileName = "DialogueContainer", menuName = "Scriptable Objects/DialogueContainer")]
public class DialogueContainer : ScriptableObject
{
    [SerializeField] Selection[] grouping;
}
