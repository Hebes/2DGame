/****************************************************
    文件：VolumeMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/31 17:15:30
	功能：URP效果参数管理
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using System;
using System.Threading.Tasks;

public class VolumeMgr : MonoBehaviour
{
    public static VolumeMgr Instance { get; private set; }
    private Volume _volume;
    private ChromaticAberration _chromaticAberration;
    private Tween _tween;
    private float _attackTargetValue = 0.3f;
    private float _hitTargetValue = 1f;
    private void Awake()
    {
        Instance = this;
        Init();
    }
    private void OnDestroy()
    {
        _tween?.Kill();
    }
    private void Init()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _chromaticAberration);
    }
    public void OpenAttackEffect(float time)
    {
        SetChromaticAberrationValue(_attackTargetValue);
        _tween?.Kill();
        _tween = DOTween
            .To(() => _chromaticAberration.intensity.value, SetChromaticAberrationValue, _attackTargetValue, time)
            .SetUpdate(true)
            .SetEase(Ease.Linear)
            .OnComplete(() => SetChromaticAberrationValue(0));
    }
    public void OpenHitEffect(float time)
    {
        SetChromaticAberrationValue(_hitTargetValue);
        _tween?.Kill();
        _tween = DOTween
            .To(() => _chromaticAberration.intensity.value, SetChromaticAberrationValue, _hitTargetValue, time)
            .SetUpdate(true)
            .SetEase(Ease.Linear)
            .OnComplete(() => SetChromaticAberrationValue(0));
    }
    private void SetChromaticAberrationValue(float value)
    {
        if (_chromaticAberration == null)
        {

            Debug.LogError($"当前Volume组件未包含ChromaticAberration效果");
            return;
        }
        _chromaticAberration.intensity.value = value;
    }

}