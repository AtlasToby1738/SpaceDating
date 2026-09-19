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
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float _currentScore = 0;
    [SerializeField]
    private float _scoreFraction;

    void OnEnable()
    {
        _currentScore = 0;
        fluidMaterial.SetFloat("_Score", 0);
        _scoreFraction = length / (maxScore * 2);
        testLahbibe.Score += ChangeScore;
        testLahbibe.OnChangeDate += Reseting;
    }

    private void OnDisable()
    {
        _currentScore = 0;
        fluidMaterial.SetFloat("_Score", 0);
        _scoreFraction = length / (maxScore * 2);
        testLahbibe.Score -= ChangeScore;
        testLahbibe.OnChangeDate -= Reseting;
    }

    private void Reseting()
    {
        StartCoroutine(ResetingFluid());
    }

    private void ChangeScore(int difference)
    {
        Debug.Log(_currentScore);
        if (difference > 0) StartCoroutine(IncreasingScore());
        else if (difference < 0) StartCoroutine(DecreasingScore());
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
        Debug.Log(_currentScore);
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
        _currentScore -= _scoreFraction;
        Debug.Log(_currentScore);
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
