using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreFluid : MonoBehaviour
{
    [SerializeField]
    private float duration = 1.0f;
    [SerializeField]
    private Material fluidMaterial;
    [SerializeField]
    private float maxScore;
    [SerializeField]
    private float length;

    private AnimationCurve curve;

    private float _currentScore = 0;
    private float _scoreFraction;
    void OnEnable()
    {
        _scoreFraction = length / maxScore;
    }

    private void ChangeScore(int difference)
    {

    }

    public IEnumerator IncreasingScore()
    {
        float _age = 0;
        curve = AnimationCurve.Linear(0, _currentScore, 1, _currentScore + _scoreFraction);
        
        while (_age<1)
        {
            _age += Time.deltaTime / duration;
            fluidMaterial.SetFloat("_Score", curve.Evaluate(_age));
            yield return null;
        }
        _currentScore += _scoreFraction;
    }
    public IEnumerator DecreasingScore()
    {
        float _age = 0;
        curve = AnimationCurve.Linear(0, _currentScore, 1, _currentScore - _scoreFraction);

        while (_age < 1)
        {
            _age += Time.deltaTime / duration;
            fluidMaterial.SetFloat("_Score", curve.Evaluate(_age));
            yield return null;
        }
        _currentScore += _scoreFraction;
    }
}
