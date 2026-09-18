using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Image[] characterImages;
    [SerializeField] private Image[] heartImages;
    [SerializeField] private Image player;
    [SerializeField] private Sprite failedPlayerSprite;
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private TMP_Text[] scoreTexts;
    [SerializeField] private CharacterAttributes[] attributes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int playerScore = 0;
        for(int Increment = 0; Increment < attributes.Length; Increment++)
        {
            scoreTexts[Increment].text = attributes[Increment].score.ToString();
            if (attributes[Increment].score >= 0)
            {
                characterImages[Increment].gameObject.SetActive(true);
                heartImages[Increment].sprite = heartSprite;
                playerScore++;
            }
        }
        scoreTexts[scoreTexts.Length - 1].text = playerScore.ToString() + " / " + attributes.Length.ToString();
        if(playerScore == 0)
        {
            player.sprite = failedPlayerSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
