/****************************************************
    文件：TipCanvas.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/24 15:22:58
	功能：提示画布
*****************************************************/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TipCanvas : MonoBehaviour 
{
    private float _animTime = 1f;
    private float _startY = 4f;
    private float _endY = 4.3f;
    private CanvasGroup _canvasGroup;
    private float _fadeAnimTime = 0.5f;
    private void Awake()
    {
        _canvasGroup = gameObject.AddOrGet<CanvasGroup>();
        SetTipCanvasActive(false);
    }
    public void Init(float animTime,float startY,float endY,float fadeAnimTime)
    {
        _animTime = animTime;
        _startY = startY;
        _endY = endY;
        _fadeAnimTime = fadeAnimTime;
    }
    public void SetTipCanvasActive(bool value)
    {
        _canvasGroup.DOKill();
        if (value)
        {
            transform.SetActive(value);
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, _fadeAnimTime)
           .SetEase(Ease.Linear)
           .SetUpdate(true);
        }
        else
        {
            _canvasGroup.DOFade(0, _animTime)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() => transform.SetActive(value));
        }
        if (value)
        {
            transform.localPosition = new Vector2(0, _startY);
            transform.DOKill();
            transform.DOLocalMoveY(_endY, _animTime)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }
    private void OnDestroy()
    {
        transform.DOKill();
        _canvasGroup.DOKill();
    }
}