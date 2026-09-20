using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public DialogueContainer dialogue;
    public CharacterAttributes attributes;
    public SpriteRenderer image;
    [SerializeField] private Animator animator;
    [SerializeField] private float emotionTime = 1.5f;
    [SerializeField] private Appearing apparingScript;

    private void OnEnable()
    {
        testLahbibe.Score += RespondToWord;
    }

    private void OnDisable()
    {
        testLahbibe.Score -= RespondToWord;
    }

    void Start()
    {
        if (image == null)
            image = GetComponentInChildren<SpriteRenderer>(true);

        if (animator == null)
            animator = GetComponentInChildren<Animator>(true);

        if (apparingScript == null)
            apparingScript = GetComponentInChildren<Appearing>(true);

        image.sprite = attributes.sprites[1];
        image.enabled = false;
        image.gameObject.SetActive(true);
    }

    public void InitializeCharacter()
    {
        attributes.score = 0;
        image.sprite = attributes.sprites[1];
        image.enabled = true;
        StartCoroutine(apparingScript.CharacterAppearing());
    }
    public void RemoveCharacter()
    {
        image.enabled = false;
    }

    public void GoAway()
    {
        StartCoroutine(apparingScript.CharacterDisappearing());
    }

    public string GetName() {return attributes.charaName;}
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
        int _type = responseType + 1;
        image.sprite = attributes.sprites[_type];
        Invoke(nameof(ReturnToNeutral), emotionTime);
        if (_type != 1) animator.SetTrigger(_type == 0? "Bad" : "Good");
    }
    private void ReturnToNeutral()
    {
        image.sprite = attributes.sprites[1];
    }
    
}
