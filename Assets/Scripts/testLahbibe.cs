using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices.WindowsRuntime;

public class testLahbibe : MonoBehaviour
{
    static public testLahbibe instance;

    [Header("Date")]
    [SerializeField] private int dateIndex = 0;
    [SerializeField] private CharacterScript[] pnj;
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
    [SerializeField] private Color[] patchColors;

    [Header("Bonus and Malus")]
    [SerializeField] private float missMaluse;
    [SerializeField] private float wordBonusTime;

    [Header("Time")]
    [SerializeField] private float timer;
    [SerializeField] private TextMeshProUGUI clock;
    [SerializeField] private float currentTime;

    [Header("Refs")]
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private Teleporter teleporter;
    [SerializeField] private Image loop;

    [Header("Game")]
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private bool isGameStopped = false;
    [SerializeField] private bool canType = true;
    [SerializeField] private Image Cursor;
    private bool isFirstReveal = true;

    // A suprimier apres les playtest
    [Header("FeedBacks")]
    [SerializeField] private Camera cam;
    [SerializeField] float timeDelay = 1f;

    public static event Action<int> Score;
    public static event Action OnChangeDate;

    private void OnEnable()
    {
        teleporter.OnTeleportArrived += HandleTeleportArrived;
    }

    private void OnDisable()
    {
        teleporter.OnTeleportArrived -= HandleTeleportArrived;
    }

    void Start()
    {
        instance = this;
        currentTime = timer;

        Initialize();
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

        StartCoroutine(teleporter.AppearingCoroutine());
    }

    void Update()
    {
        if (isGameOver || isFirstReveal) return;

        if (currentTime <= 0)
        {
            isGameOver = true;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                    if (letterIndex[currentWordIndex] == wordLetters.Length)
                    {
                        AddOrSubScore(wordsScores[currentWordIndex]);
                        AddOrSubTime(wordBonusTime);
                        currentWordIndex = 0;
                        text.text = "";
                        SetCursor();

                        int wordCount = pnj[dateIndex].dialogue.grouping[sentenceIndex].type[0].word.Length;
                        int sentenceCount = pnj[dateIndex].dialogue.grouping.Length;

                        if (wordIndex < wordCount - 1)
                        {
                            //Debug.Log("il reste des mots dans la phrase actuelle");

                            wordIndex++;
                            SetWords();
                        }
                        else if (sentenceIndex < sentenceCount - 1)
                        {
                            //Debug.Log("plus de mots dans cette phrase, mais il reste des phrases");

                            wordIndex = 0;
                            ChangeSentence();
                            SetWords();
                        }
                        else
                        {
                            //Debug.Log("plus de mots ni de phrases -> date suivante");

                            wordIndex = 0;
                            sentenceIndex = 0;
                            currentWordIndex = 0;
                            ChangeDate();
                        }

                        return;
                    }
                }

                else if (c == wordLetters[letterIndex[currentWordIndex]])
                {
                    text.text += c;
                    letterIndex[currentWordIndex]++;
                    if (char.IsWhiteSpace(c) == false)
                    {
                        hiddenWord[currentWordIndex].RevealNextLetter();
                    }
                    return;
                    // sond --------------------------------------------------------------------
                }

                MissTheKey(c);
            }
        }
    }

    private void MissTheKey(char _letter)
    {
        text.text += _letter;
        canType = false;
        foreach (var image in hiddenWord[currentWordIndex].imageArray)
        {
            image.color = patchColors[1];
        }
        AddOrSubTime(missMaluse);

        Invoke(nameof(StopMissEffect), timeDelay);
    }

    void StopMissEffect()
    {
        text.text = text.text.Remove(text.text.Length - 1, 1);

        cam.backgroundColor = Color.white;
        foreach (var image in hiddenWord[currentWordIndex].imageArray)
        {
            image.color = patchColors[0];
        }

        canType = true;
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

        //if (currentTime < 0) // sond --------------------------------------------------------------------

        // ajouter l'effet genre pop up, sond, vibrasion
    }

    private void AddOrSubScore(int _score)
    {
        Score?.Invoke(_score);
        score += _score;
        Debug.Log(score);
        // sond --------------------------------------------------------------------

        // charaters[dateIndex].attributes.score += _score; --------------------------------
    }



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

    private void ChangeDate()
    {
        StartCoroutine(teleporter.AppearingCoroutine());
        pnj[dateIndex].GoAway();
    }

    private void SetDate()
    {
        nameDisplay.text = pnj[dateIndex].GetName();
        Debug.Log(pnj[dateIndex].GetName());
        currentTime = timer;
        SetSentence();
    }

    private void HandleTeleportArrived()
    {
        Debug.Log($"HandleTeleportArrived appelé, dateIndex avant = {dateIndex}");

        if (isFirstReveal)
        {
            isFirstReveal = false;
            pnj[dateIndex].InitializeCharacter();
            float _cursorLocation = hiddenWord[0].transform.position.y;
            Cursor.transform.position = new Vector3(Cursor.transform.position.x, _cursorLocation, 0);
            SetDate();
            SetWords();
            loop.gameObject.SetActive(true);
            return;
        }

        pnj[dateIndex].RemoveCharacter();
        pnj[dateIndex].attributes.score = score;
        Debug.Log(score);
        score = 0;

        dateIndex++;
        if (dateIndex >= pnj.Length)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        pnj[dateIndex].InitializeCharacter();
        OnChangeDate?.Invoke();

        sentenceIndex = 0;
        wordIndex = 0;
        currentWordIndex = 0;

        SetDate();
        SetWords();
    }

    private void SetWords()
    {
        for (int i = 0; i < 3; i++)
        {
            letterIndex[i] = 0;
            wordsScores[i] = 0;
        }

        var types = pnj[dateIndex].dialogue.grouping[sentenceIndex].type;

        var pool = new List<int>();
        for (int i = 0; i < types.Length; i++) pool.Add(i);

        for (int i = 0; i < types.Length; i++)
        {
            int pick = UnityEngine.Random.Range(0, pool.Count);
            int typeIndex = pool[pick];
            pool.RemoveAt(pick);

            currentWords[i] = types[typeIndex].word[wordIndex];
            wordsScores[i] = typeIndex - 1;
        }

        wordLetters = currentWords[0].ToCharArray();

        for (int j = 0; j < 3; j++)
        {
            hiddenWord[j].SetNewWord(currentWords[j]);
        }
    }
}