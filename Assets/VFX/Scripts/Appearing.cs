using System.Collections;
using UnityEngine;

public class Appearing : MonoBehaviour
{
    [SerializeField]
    private float duration = 1;
    [SerializeField]
    private AnimationCurve appearingCurve;
    [SerializeField]
    private Material material;
    private void OnEnable()
    {
        StartCoroutine(CharacterAppearing());
    }
    private IEnumerator CharacterAppearing()
    {
        float _age = 0;
        while(_age<1)
        {
            _age += Time.deltaTime / duration;
            material.SetFloat("_Apparition",appearingCurve.Evaluate(_age));

        yield return null;
        }
    }
}
