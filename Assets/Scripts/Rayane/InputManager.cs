using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    [SerializeField] private char input = '.';
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.inputString.Length == 0 || input == Input.inputString[0]) return;
            input = Input.inputString[0];
            Debug.Log(input);
        }
        else input = '.';
    }
}
