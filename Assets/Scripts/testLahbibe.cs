using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class testLahbibe : MonoBehaviour
{
    [SerializeField] private int dateIndex = 1;
    [SerializeField] private float timer;
    private float currentTime;
    [SerializeField] private string[] words;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int index = 0;
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

        foreach (char c in Input.inputString)
        {
            if (Input.anyKeyDown)
            {
                if (c == '\b')
                {
                    if (text.text.Length != 0)
                    {
                        text.text = text.text.Substring(0, text.text.Length - 1);
                        return;
                    }
                }
                else if ((c == '\n') || (c == '\r'))
                {
                    if (index >= words.Length)
                    {
                        Debug.Log(WordValidation());
                        text.text = "";
                    }
                }
                else
                {
                    text.text += c;
                }
            }
        }
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
        return true;
    }
}
