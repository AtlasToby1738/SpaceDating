using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class testLahbibe : MonoBehaviour
{
    [SerializeField] private int dateIndex = 1;
    [SerializeField] private float timer;
    private float currentTime;
    [SerializeField] private string[] words;
    [SerializeField] private char[] wordLetters;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TestRayane hiddenWord;
    [SerializeField] private int index = 0;
    [SerializeField] private int letterIndex = 0;
    private string writtenWord;
    private bool isGameOver = false;
    private bool canType = true;

    // A suprimier apres les playtest
    [SerializeField] private Camera cam;
    [SerializeField] float timeDelay = 1f;

    void Start()
    {
        currentTime = timer;
        SetWord();
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            isGameOver = true;
        }

        foreach (char c in Input.inputString)
        {
            if (Input.anyKeyDown)
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
                    if (index <= words.Length && letterIndex == wordLetters.Length)
                    {
                        Debug.Log(WordValidation());
                        text.text = "";
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
                    //else
                    //{ 
                    //    text.text += c;
                    //    //canType = false;
                    //    //cam.backgroundColor = Color.red;
                    //    //foreach(var image in hiddenWord.imageArray)
                    //    //{
                    //    //    image.color = Color.red;
                    //    //}

                    //    //Invoke(nameof(ResetError), timeDelay);
                        
                    //}
                }
            }
        }
    }

    void ResetError()
    {
        text.text.Remove(letterIndex);
        cam.backgroundColor = Color.red;
        foreach (var image in hiddenWord.imageArray)
        {
            image.color = Color.red;
        }
        canType = true;
    }

    private void SetWord()
    {
        letterIndex = 0;
        wordLetters = words[index].ToCharArray();
        hiddenWord.SetNewWord(words[index]);
    }

    private bool WordValidation()
    {
        // ici on aura besoin de changé par rapport au 3 mots au lieu d'un seul avec un for each des 3 et a partir du moment ou c'est vrai on continue
        Debug.Log(words[index].Length);
        Debug.Log(text.text.Length);

        if (words[index].Length != text.text.Length)
        {
            Debug.Log("on s'arrete au nombre de char");
            return false;
        }

        char[] wordChars = words[index].ToCharArray();
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
        index++;
        Debug.Log(index);
        SetWord();
        return true;
    }

    //private void
}
