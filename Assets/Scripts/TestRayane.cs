using System;
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

    public void Start()
    {
        testText = GetComponent<TMP_Text>();
        testText.ForceMeshUpdate();
        Vector3[] verticeArray;
        verticeArray = testText.mesh.vertices;
        Debug.Log(verticeArray.Length);
        Debug.Log(testText.text.Length);
        int spaceDetection = 0;
        for (int Increment = 0; Increment < testText.text.Length - spaceDetection; Increment++)
        {
            int newEnum = UnityEngine.Random.Range(1, 4);
            testHiddenArea = (EHiddenArea)newEnum;
            if (testText.textInfo.characterInfo[Increment].character == ' ') spaceDetection++;
            Image testImage = Instantiate<Image>(testImagePrefab);
            testImage.transform.SetParent(testText.transform, false);
            testImage.rectTransform.position = verticeArray[Increment * 4] + testText.transform.position;
            switch(testHiddenArea)
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
            Debug.Log(verticeArray[Increment]);
            //testText.textInfo.characterInfo[Increment].
        }
    }
}