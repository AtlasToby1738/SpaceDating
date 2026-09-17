using UnityEngine;
using System.Collections.Generic;

public struct Answer
{
    public string first;
    public string second;
    public string troisieme;
    public string quatrieme;
    public string cinquieme;
}

public class Character : MonoBehaviour
{
    public int index;
    [SerializeField] string[] questions;
    [SerializeField] string[] answers1_1;
    [SerializeField] string[] answers1_2;
    [SerializeField] string[] answers1_3;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
