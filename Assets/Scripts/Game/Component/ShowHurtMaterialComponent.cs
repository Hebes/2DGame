/****************************************************
    文件：ShowHurMaterialComponent.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/15 11:14:51
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShowHurtMaterialComponent : MonoBehaviour 
{
    private SpriteRenderer _render;
    private float _showHitEffectTime = 0.2f;
    private float _lastShowHitEffectTime;//最后一次显示受伤特效的时间
    private bool _isShowEffect;
    public void Init(SpriteRenderer render, float showHitEffectTime = 0.2f)
    {
        _render = render;
        _showHitEffectTime = showHitEffectTime;
    }
    private void Update()
    {
        CheckHitEffectOver();
    }
    //显示受伤特效
    public void ShowHitEffect()
    {
        if (_render == null)
        {
            Debug.LogError($"该组件未初始化,或者初始化为成功");
            return;
        }
        SetHurtMatericalValue(1f);
        _lastShowHitEffectTime = Time.unscaledTime;
        _isShowEffect = true;
    }
    private void CheckHitEffectOver()
    {
        if(_isShowEffect&&Time.unscaledTime>=_lastShowHitEffectTime+_showHitEffectTime)
        {
            SetHurtMatericalValue(0f);
            _isShowEffect = false;
        }
    }
    private void SetHurtMatericalValue(float value)
        => _render.material.SetFloat(Consts.MATERIAL_ARGNAME_HURT, value);
}