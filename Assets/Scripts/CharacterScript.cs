using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    public DialogueContainer dialogue;
    public CharacterAttributes attributes;
    public Image image;
    [SerializeField] private Animator animator;
    [SerializeField] private float emotionTime = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponentInChildren<Image>(true);
        image.sprite = attributes.sprites[1];
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeCharacter()
    {
        image.sprite = attributes.sprites[1];
        image.enabled = true;
    }
    public void RemoveCharacter()
    {
        image.sprite = attributes.sprites[1];
        image.enabled = false;
    }
    public string GetName() {return attributes.name;}
    public string[] GetWordTrio(int groupingIndex, int wordIndex, ref bool endOfDate, ref bool endOfSentence)
    {
        string[] trio =
        {
            dialogue.grouping[groupingIndex].type[0].word[wordIndex],
            dialogue.grouping[groupingIndex].type[1].word[wordIndex],
            dialogue.grouping[groupingIndex].type[2].word[wordIndex]
        };
        if(dialogue.grouping.Length - 1 <= groupingIndex) endOfDate = true;
        if (dialogue.grouping[groupingIndex].type[0].word.Length - 1 <= wordIndex) endOfSentence = true;
        return trio;
    }
    
    public void RespondToWord(int responseType)
    {
        image.sprite = attributes.sprites[responseType];
        Invoke(nameof(ReturnToNeutral), emotionTime);
        if (responseType != 1) animator.SetTrigger(responseType == 0? "Bad" : "Good");
    }
    private void ReturnToNeutral()
    {
        image.sprite = attributes.sprites[1];
    }
    
}
