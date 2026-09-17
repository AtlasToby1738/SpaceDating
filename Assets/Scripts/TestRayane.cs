using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestRayane : MonoBehaviour
{
    public TMP_Text testText;
    public Image testImagePrefab;

    public void Start()
    {
        testText.ForceMeshUpdate();
        Vector3[] verticeArray;
        verticeArray = testText.mesh.vertices;
        Debug.Log(verticeArray.Length);
        Debug.Log(testText.text.Length);
        int spaceDetection = 0;
        for (int Increment = 0; Increment < testText.text.Length - spaceDetection; Increment++)
        {
            if (testText.textInfo.characterInfo[Increment].character == ' ') spaceDetection++;
            Image testImage = Instantiate<Image>(testImagePrefab);
            testImage.transform.SetParent(testText.transform, false);
            testImage.rectTransform.position = (verticeArray[Increment * 4] + verticeArray[Increment * 4 + 1]) / 2 + testText.transform.position;
            Debug.Log(Increment * 4);
            Debug.Log(Increment * 4 + 1);

            RectTransform testRect;
            //testText.textInfo.characterInfo[Increment].
        }
    }
}