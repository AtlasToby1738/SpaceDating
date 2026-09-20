using System.Collections;
using UnityEngine;

public class Appearing : MonoBehaviour
{
    [SerializeField]
    private float duration = 1;
    [SerializeField]
    private AnimationCurve appearingCurve;
    [SerializeField]
    private AnimationCurve disappearingCurve;
    [SerializeField]
    private Material material;
    public IEnumerator CharacterAppearing()
    {
        material.SetFloat("_pisitiveY", 1);

        float _age = 0;
        while (_age < 1)
        {
            _age += Time.deltaTime / duration;
            material.SetFloat("_Apparition", appearingCurve.Evaluate(_age));

            yield return null;
        }
    }

    public IEnumerator CharacterDisappearing()
    {
        material.SetFloat("_pisitiveY", 0);

        float _age = 0;
        while(_age<1)
        {
            _age += Time.deltaTime / duration;
            material.SetFloat("_Apparition", disappearingCurve.Evaluate(_age));

        yield return null;
        }
    }
}
