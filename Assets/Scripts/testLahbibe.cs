using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class testLahbibe : MonoBehaviour
{
    [SerializeField] private float timer;
    private float currentTime;
    [SerializeField] private string[] words;
    [SerializeField] private TextMeshProUGUI text;
    private string writtenWord;
    private bool isGameOver = false;

    void Start()
    {
        currentTime = timer;
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            isGameOver = true;
        }

        if (Input.anyKeyDown)
        {
            
            text.text += Input.inputString;
        }
    }
}
