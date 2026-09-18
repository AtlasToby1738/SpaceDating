using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Image[] characterImages;
    [SerializeField] private Image player;
    [SerializeField] private Sprite failedPlayerSprite;
    [SerializeField] private TMP_Text text;
    [SerializeField] private CharacterAttributes[] attributes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool isAlone = true;
        for(int Increment = 0; Increment < attributes.Length; Increment++)
        {
            if (attributes[Increment].score >= 0)
            {
                characterImages[Increment].enabled = true;
                isAlone = false;
            }
        }
        if(isAlone)
        {
            player.sprite = failedPlayerSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
