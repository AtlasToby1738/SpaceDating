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
        testLahbibe.instance.Score += ChangeScore;
    }

    private void ChangeScore(int difference)
    {
        if (difference > 0) IncreasingScore();
        else if (difference < 0) DecreasingScore();
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
    public IEnumerator ResetingFluid()
    {
        float _age = 0;
        curve = AnimationCurve.Linear(0, _currentScore, 1, 0);

        while (_age < 1)
        {
            _age += Time.deltaTime / duration;
            fluidMaterial.SetFloat("_Score", curve.Evaluate(_age));
            yield return null;
        }
        _currentScore = 0;
    }
}
