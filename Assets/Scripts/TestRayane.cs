using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EHiddenArea : int
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class TestRayane : MonoBehaviour
{
    public TMP_Text testText;
    public Image testImagePrefab;
    public EHiddenArea testHiddenArea;
    private List<Image> imageArray = new List<Image>();

    public void Start()
    {
        testText = GetComponent<TMP_Text>();
        //CODE TEST A CHANGER
        SetNewWord("GnorpGlorp");
    }
    
    public void SetNewWord(string Word)
    {
        while(imageArray.Count > 0)
        {
            Destroy(imageArray[0]);
            imageArray.RemoveAt(0);
        }
        if (Word.Length <= 0) return;
        testText.text = Word;
        testText.ForceMeshUpdate();
        Vector3[] verticeArray;
        verticeArray = testText.mesh.vertices;
        int spaceDetection = 0;
        for (int Increment = 0; Increment < testText.text.Length - spaceDetection; Increment++)
        {
            EHiddenArea newEnum = EHiddenArea.NONE;
            if (testHiddenArea == EHiddenArea.NONE) newEnum = (EHiddenArea)UnityEngine.Random.Range(1, 4);
            else newEnum = testHiddenArea;
            if (testText.textInfo.characterInfo[Increment].character == ' ') spaceDetection++;
            Image testImage = Instantiate<Image>(testImagePrefab);
            testImage.transform.SetParent(testText.transform, false);
            testImage.rectTransform.position = verticeArray[Increment * 4] + testText.transform.position;
            switch (newEnum)
            {
                case EHiddenArea.UP:
                {
                    testImage.rectTransform.position += new Vector3(0.0f, (verticeArray[Increment * 4 + 1].y - verticeArray[Increment * 4].y) / 2, 0.0f);
                    testImage.rectTransform.sizeDelta = new Vector2((verticeArray[Increment * 4 + 3].x - verticeArray[Increment * 4].x), (verticeArray[Increment * 4 + 1].y - verticeArray[Increment * 4].y) / 2);
                    break;
                }
                case EHiddenArea.DOWN:
                {
                    testImage.rectTransform.sizeDelta = new Vector2((verticeArray[Increment * 4 + 3].x - verticeArray[Increment * 4].x), (verticeArray[Increment * 4 + 1].y - verticeArray[Increment * 4].y) / 2);
                    break;
                }
                case EHiddenArea.LEFT:
                {
                    testImage.rectTransform.position += new Vector3((verticeArray[Increment * 4 + 2].x - verticeArray[Increment * 4 + 1].x) / 2, 0.0f, 0.0f);
                    testImage.rectTransform.sizeDelta = new Vector2((verticeArray[Increment * 4 + 3].x - verticeArray[Increment * 4].x) / 2, (verticeArray[Increment * 4 + 1].y - verticeArray[Increment * 4].y));
                    break;
                }
                case EHiddenArea.RIGHT:
                {
                    testImage.rectTransform.sizeDelta = new Vector2((verticeArray[Increment * 4 + 3].x - verticeArray[Increment * 4].x) / 2, (verticeArray[Increment * 4 + 1].y - verticeArray[Increment * 4].y));
                    break;
                }
            }
            imageArray.Add(testImage);
        }
    }

    public void RevealNextLetter()
    {
        Destroy(imageArray[0]);
        imageArray.RemoveAt(0);
    }
}