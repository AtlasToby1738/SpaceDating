using System;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    public event Action<char> OnType;
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
            char input;
            if (Input.inputString.Length == 0) return;
            input = Input.inputString[0];
            OnType?.Invoke(input);
            Debug.Log(input);
        }
    }
}
