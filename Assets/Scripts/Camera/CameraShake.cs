using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Vector3 _posStrength;
    [SerializeField] private Vector3 _rotStrength;

    private static event Action Shake;

    public static void Invoke()
    {
        Shake?.Invoke();
    }

    private void OnEnable() => Shake += CameraShaker;
    private void OnDisable() => Shake -= CameraShaker;


    private void CameraShaker()
    {
        _cam.DOComplete();
        _cam.DOShakePosition(0.3f, _posStrength);
        _cam.DOShakeRotation(0.3f, _rotStrength);


    }
}
