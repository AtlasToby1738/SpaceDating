using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class testLahbibe : MonoBehaviour
{
    [Header("Date")]
    [SerializeField] private int dateIndex = 0;
    [SerializeField] private CharacterScript[] pnj;
    [SerializeField] private string name;
    [SerializeField] private TextMeshProUGUI nameDisplay;


    [Header("Dialogue")]
    [SerializeField] private List<string> sentences;
    [SerializeField] private TextMeshProUGUI sentencesDisplay;
    [SerializeField] private string[] currentWords;
    [SerializeField] private int[] wordsScores;
    [SerializeField] private int score = 0;
    [SerializeField] private char[] wordLetters;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TestRayane[] hiddenWord;
    [SerializeField] private int wordIndex = 0;
    [SerializeField] private int[] letterIndex;
    [SerializeField] private int sentenceIndex = 0;
    [SerializeField] private GameObject[] cursorPositions;
    private string writtenWord;

    [Header("Writting")]
    [SerializeField] private int speed;
    [SerializeField] private int letterCount = 0;
    [SerializeField] private int currentWordIndex = 0;

    [Header("Bonus and Malus")]
    [SerializeField] private float missMaluse;
    [SerializeField] private float wordBonusTime;

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
    [SerializeField] private Image Cursor;

    // A suprimier apres les playtest
    [Header("FeedBacks")]
    [SerializeField] private Camera cam;
    [SerializeField] float timeDelay = 1f;

    static Action<int> Score;

    void Start()
    {
        currentTime = timer;

        Initialize();

        //StartCoroutine(Display());

        SetWords();
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            currentWordIndex++;

            Debug.Log(currentWordIndex);

            if (currentWordIndex >= 3)
            {
                currentWordIndex = 0;
            }

            SetCursor();

            wordLetters = currentWords[currentWordIndex].ToCharArray();
        }

        foreach (char c in Input.inputString)
        {
            if (Input.anyKeyDown && canType && !isGameStopped)
            {
                if ((c == '\n') || (c == '\r'))
                {
                    Debug.Log(wordIndex);
                    if (wordIndex < pnj[dateIndex].dialogue.grouping[sentenceIndex].type[0].word.Length - 1 && letterIndex[currentWordIndex] == wordLetters.Length)
                    {
                        Debug.Log("-----------------------------------------------------------");

                        AddOrSubScore(wordsScores[currentWordIndex]);
                        AddOrSubTime(wordBonusTime);
                        wordIndex++;
                        SetWords();
                        text.text = "";
                        currentWordIndex = 0;
                        SetCursor();
                        // ajouter des effets visuels
                    }

                    if (letterIndex[currentWordIndex] == wordLetters.Length && wordIndex <= pnj[dateIndex].dialogue.grouping[sentenceIndex].type[0].word.Length - 1 && sentenceIndex < pnj[dateIndex].dialogue.grouping.Length - 1)
                    {
                        Debug.Log("LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");

                        AddOrSubScore(wordsScores[currentWordIndex]);
                        AddOrSubTime(wordBonusTime);
                        wordIndex = 0;
                        text.text = "";
                        currentWordIndex = 0;
                        SetCursor();
                        ChangeSentence();
                        SetWords();//-------------------------------------------------------------------
                    }
                    if (sentenceIndex <= pnj[dateIndex].dialogue.grouping.Length && letterIndex[currentWordIndex] == wordLetters.Length)
                    {
                        Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++");
                        AddOrSubScore(wordsScores[currentWordIndex]);
                        AddOrSubTime(wordBonusTime);
                        wordIndex = 0;
                        text.text = "";
                        currentWordIndex = 0;
                        SetCursor();
                        ChangeDate();
                    }
                }
                else
                {
                    Debug.Log(letterIndex[currentWordIndex]);
                    Debug.Log("/");
                    Debug.Log(currentWords[currentWordIndex].Length);
                    if (letterIndex[currentWordIndex] < currentWords[currentWordIndex].Length && c == wordLetters[letterIndex[currentWordIndex]])
                    {
                        text.text += c;
                        letterIndex[currentWordIndex]++;
                        if (char.IsWhiteSpace(c) == false)
                        {
                            hiddenWord[currentWordIndex].RevealNextLetter();
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

    private void ChangeDate()
    {
        dateIndex ++;
        if (dateIndex >= 3) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // activer le teleporteur
        // set les characteur, apparaitre et disparaitre

        SetDate();

        SetWords();
    }


    private void MissTheKey(char _letter)
    {
        text.text += _letter;
        canType = false;
        cam.backgroundColor = Color.red;
        foreach (var image in hiddenWord[currentWordIndex].imageArray)
        {
            image.color = Color.red;
        }

        AddOrSubTime(missMaluse);

        Invoke(nameof(StopMissEffect), timeDelay);
    }

    private void SetCursor()
    {
        float _cursorLocation = hiddenWord[currentWordIndex].gameObject.transform.position.y;
        Cursor.transform.position = new Vector3(Cursor.transform.position.x, _cursorLocation, 0);
    }

    private void SetSentence()
    {
        sentencesDisplay.text = pnj[dateIndex].dialogue.grouping[sentenceIndex].characterSentence;
    }

    private void ChangeSentence()
    {
        sentenceIndex++;
        SetSentence();
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

    private void AddOrSubScore(int _score)
    {
        // Score.Invoke(_score); ERROR EXEPTION
        // charaters[dateIndex].attributes.score += _score; --------------------------------
    }

    void StopMissEffect()
    {
        text.text = text.text.Remove(text.text.Length - 1, 1);

        cam.backgroundColor = Color.white;
        foreach (var image in hiddenWord[currentWordIndex].imageArray)
        {
            image.color = Color.white;
        }

        canType = true;
    }

    private void Initialize()
    {

        foreach (var character in pnj)
        {
            for (int i = 0; i < pnj[dateIndex].dialogue.grouping.Length; i++)
            {
                sentences.Add(character.dialogue.grouping[i].characterSentence);
            }
        }

        float _cursorLocation = hiddenWord[0].transform.position.y;
        Cursor.transform.position = new Vector3(Cursor.transform.position.x, _cursorLocation, 0);

        Debug.Log("ici");
        SetDate();
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
    private void SetDate()
    {
        name = pnj[dateIndex].attributes.name;
        nameDisplay.text = name;
        SetSentence();
    }

    private void SetWords()
    {
        for(int i =0; i < 3; i++)
        {
           letterIndex[i] = 0;
        }

        for (int i = 0; i < pnj[dateIndex].dialogue.grouping[sentenceIndex].type[0].word.Length; i++)
        {
            Debug.Log(dateIndex);
            Debug.Log(sentenceIndex);
            Debug.Log(wordIndex);

            Debug.Log(pnj[dateIndex].dialogue.grouping[sentenceIndex].type[i].word[wordIndex]);
            currentWords[i] = pnj[dateIndex].dialogue.grouping[sentenceIndex].type[i].word[wordIndex];
        }

        wordLetters = currentWords[0].ToCharArray();

        for (int j = 0; j < 3; j++)
        {
            hiddenWord[j].SetNewWord(currentWords[j]);
        }
    }
}
