/****************************************************
    文件：HpItem.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2022/1/12 16:33:56
	功能：游戏中的每个血条
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class HpItem : MonoBehaviour
{
    private Image _img;
    private float _animTime = 1f;
    private float _shakeForce = 6f;
    private int _shakeVibrato = 70;
    private float _shakeRandom = 180;
    private bool _shakeSnapping = false;
    private bool _shakeFade = true;

    public void Init()
    {
        _img = GetComponent<Image>();
    }
    public void SetActive(bool value)
    {
        if (gameObject.activeSelf == value)
            return;
        _img.DOKill();
        _img.transform.DOKill();
        var color = _img.color;
        if (value)
        {
            gameObject.SetActive(value);
            color.a = 0f;
            _img.color = color;
            _img.DOFade(1, _animTime)
                .SetEase(Ease.OutCirc)
                .SetUpdate(false);
            _img.transform.localScale = Vector3.zero;
            _img.transform.DOScale(Vector3.one,_animTime/3f);
            _img.transform.DOShakePosition(_animTime, _shakeForce, _shakeVibrato, _shakeRandom, _shakeSnapping, _shakeFade)
              .SetUpdate(false);
        }
        else
        {
            color.a = 1f;
            _img.color = color;
            _img.DOFade(0, _animTime)
                 .SetEase(Ease.InCirc)
                 .SetUpdate(true);
            _img.transform.DOShakePosition(_animTime, _shakeForce, _shakeVibrato, _shakeRandom, _shakeSnapping, _shakeFade)
                  .SetUpdate(false)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}