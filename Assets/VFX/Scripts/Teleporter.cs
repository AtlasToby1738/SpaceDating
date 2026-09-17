using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool _hasAlreadyAppeared=false ;

    [SerializeField]
    private Material teleporterMaterial;
    [SerializeField]
    private float duration;
    [SerializeField]
    private AnimationCurve appearingCurve;
    [SerializeField]
    private ParticleSystem[] HeartBurst;
    [SerializeField]
    private float minHeight = -6.75f;
    [SerializeField]
    private float maxHeight = 1.85f;
    [SerializeField]
    private Material heartMaterial;
    [SerializeField]
    private AnimationCurve heartCurve;

    private void Start()
    {
        StartCoroutine(AppearingCoroutine());
    }
    public void CheckTeleporter()
    {
        if (_hasAlreadyAppeared)
        {
            StartCoroutine(DisappearingCoroutine());
        }
        else
        {
            StartCoroutine(AppearingCoroutine());
        }
    }
    private IEnumerator AppearingCoroutine()
    {
        
        int _i = 0;
        teleporterMaterial.SetFloat("_Appear", 1);
        float _age = 0;
        while (_age < 1)
        {
            _age += Time.deltaTime / duration;
            teleporterMaterial.SetFloat("_ApparitionProgression", appearingCurve.Evaluate(_age));
            float _currentFloat = teleporterMaterial.GetFloat("_ApparitionProgression");
            heartMaterial.SetFloat("_Progression",heartCurve.Evaluate(_age));

            #region HeartBurst
            if (_i == 0 && _currentFloat >= minHeight )
            {
                _i++;
                HeartBurst[0].Play();
                yield return null;
            }
            if(_i == 1 && _currentFloat >= (minHeight - maxHeight)/2  )
            {
                
                _i++;
                HeartBurst[1].Play();
                yield return null;
            }
            if( _i==2 && _currentFloat == maxHeight)
            {
                
                _i++;
                HeartBurst[2].Play();
                yield return null;
            }
            #endregion

            yield return null;
        }
        _hasAlreadyAppeared = true;
    }
    private IEnumerator DisappearingCoroutine()
    {
        teleporterMaterial.SetFloat("_Appear", 0);
        float _age = 0;
        while (_age < 1)
        {
            _age += Time.deltaTime / duration;
            teleporterMaterial.SetFloat("_ApparitionProgression", -1*appearingCurve.Evaluate(_age));
            yield return null;
        }
        _hasAlreadyAppeared= false;
    }
}
