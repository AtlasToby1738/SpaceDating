using UnityEngine;
using System.Collections;

public class StaticScreenShake : MonoBehaviour
{
    public static IEnumerator ScreenShake(float duration, AnimationCurve curve, float intensity)
    {
        Vector3 _originalPosition = Camera.main.transform.localPosition;
        float _currentTime = 0.0f;

        while (_currentTime < duration)
        {
            _currentTime += Time.deltaTime;
            float _shakeStrength = curve.Evaluate(_currentTime / duration) * intensity;

            Camera.main.transform.localPosition = _originalPosition + Random.insideUnitSphere * _shakeStrength;
            yield return null;
        }
        Camera.main.transform.localPosition = _originalPosition;
    }
}
