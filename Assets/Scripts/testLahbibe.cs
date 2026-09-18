using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testLahbibe : MonoBehaviour
{
    [Header("Date")]
    [SerializeField] private int dateIndex = 1;
    [SerializeField] private CharacterScript[] charaters;

    [Header("Dialogue")]
    [SerializeField] private List<string> sentences;
    [SerializeField] private string[] currentWords;
    [SerializeField] private int[] wordsScores;
    [SerializeField] private int score = 0;
    [SerializeField] private char[] wordLetters;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TestRayane hiddenWord;
    [SerializeField] private int wordIndex = 0;
    [SerializeField] private int letterIndex = 0;
    private string writtenWord;

    [Header("Writting")]
    [SerializeField] private int speed;
    [SerializeField] private int letterCount = 0;

    [Header("Bonus and Malus")]
    [SerializeField] private float missMaluse;
    [SerializeField] private float wordBonus;

    [Header("Time")]
    [SerializeField] private float timer;
    [SerializeField] private TextMeshProUGUI clock;
    [SerializeField] private float currentTime;

    [Header("Refs")]
    [SerializeField] private MenuManager menuManager;

    [Header("Game")]
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private bool isGameStopped = false;
    [SerializeField] private bool canType = true;

    // A suprimier apres les playtest
    [Header("FeedBacks")]
    [SerializeField] private Camera cam;
    [SerializeField] float timeDelay = 1f;

    void Start()
    {
        currentTime = timer;

        Initialize();

        //StartCoroutine(Display());

        SetWord();



    }

    void Update()
    {
        if (isGameOver) return;

        if (currentTime <= 0)
        {
            isGameOver = true;
            cam.backgroundColor = Color.black;
            return;
        }

        currentTime -= Time.deltaTime;
        clock.text = Mathf.RoundToInt(currentTime).ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGamePaused();
        }

        foreach (char c in Input.inputString)
        {
            if (Input.anyKeyDown && canType && !isGameStopped)
            {
                if (c == '\b')
                {
                    Debug.Log("backslash mais dans le vide");

                    //if (text.text.Length != 0)
                    //{
                    //    letterIndex--;
                    //    text.text = text.text.Substring(0, text.text.Length - 1);
                    //    return;
                    //}
                }
                else if ((c == '\n') || (c == '\r'))
                {
                    if (wordIndex <= currentWords.Length && letterIndex == wordLetters.Length)
                    {
                        Debug.Log(WordValidation());
                        text.text = "";
                        // regarder par rapport au mot (si +1, 0, -1) et par rapport au gd
                        // ajouter des effets visuels
                        AddOrSubTime(wordBonus);
                    }
                }
                else
                {
                    if (c == wordLetters[letterIndex])
                    {
                        text.text += c;
                        letterIndex++;
                        if (char.IsWhiteSpace(c) == false)
                        {
                            hiddenWord.RevealNextLetter();
                        }
                    }
                    else
                    {
                        MissTheKey(c);
                    }
                }
            }
        }
    }

    private void MissTheKey(char _letter)
    {
        text.text += _letter;
        canType = false;
        cam.backgroundColor = Color.red;
        foreach (var image in hiddenWord.imageArray)
        {
            image.color = Color.red;
        }

        AddOrSubTime(missMaluse);

        Invoke(nameof(StopMissEffect), timeDelay);


    }

    public void SetGamePaused()
    {
        Debug.Log("Pause Key");
        menuManager.PauseMenu(!isGameStopped);
        isGameStopped = !isGameStopped;
        Time.timeScale = isGameStopped ? 0 : 1;
        Debug.Log(Time.timeScale);
    }

    private void AddOrSubTime(float _time)
    {
        currentTime += _time;
        // ajouter l'effet genre pop up, sond, vibrasion
    }

    void StopMissEffect()
    {
        text.text = text.text.Remove(text.text.Length - 1, 1);

        cam.backgroundColor = Color.white;
        foreach (var image in hiddenWord.imageArray)
        {
            image.color = Color.white;
        }

        canType = true;
    }

    private void Initialize()
    {
        foreach (var character in charaters)
        {
            for (int i = 0; i < 3; i++)
            {
                sentences.Add(character.dialogue.grouping[i].characterSentence);
            }
        }
    }

    //IEnumerator Display()
    //{
    //    foreach 
    //}

    private bool Write(string _text, TextMeshProUGUI _display)
    {
        if (letterCount == 0) _display.text = "";

        if (letterCount <= _text.Length)
        {
            _display.text += _text[letterCount];
            letterCount++;
            return true;
        }

        letterCount = 0;
        return false;
    }


    private void SetWord()
    {
        letterIndex = 0;
        wordLetters = currentWords[wordIndex].ToCharArray();
        hiddenWord.SetNewWord(currentWords[wordIndex]);
    }

    private bool WordValidation()
    {
        // ici on aura besoin de changé par rapport au 3 mots au lieu d'un seul avec un for each des 3 et a partir du moment ou c'est vrai on continue
        Debug.Log(currentWords[wordIndex].Length);
        Debug.Log(text.text.Length);

        if (currentWords[wordIndex].Length != text.text.Length)
        {
            Debug.Log("on s'arrete au nombre de char");
            return false;
        }

        char[] wordChars = currentWords[wordIndex].ToCharArray();
        char[] textChars = text.text.ToCharArray();
        //Debug.Log(wordChars.Length);
        //Debug.Log(textChars.Length);
        int mistakes = 0;

        for (int i = 0; i < wordChars.Length; i++)
        {
            Debug.Log(i);

            if (wordChars[i] != textChars[i])
            {
                Debug.Log("t'as fait une faute la");
                mistakes++;
            }
        }

        // Il faudra changé avec une enum pour dire faux, vrai ou neutre. ou bien faire avec la valeur du mots
        if (mistakes >= 2)
        {
            Debug.Log("il y a plus que 2 erreur");
            mistakes = 0;
            return false;
        }
        wordIndex++;
        Debug.Log(wordIndex);
        SetWord();
        return true;
    }

    //private void
}
