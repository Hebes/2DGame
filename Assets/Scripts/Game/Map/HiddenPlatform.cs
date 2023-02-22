/****************************************************
    文件：HiddenPlatform.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/27 13:23:13
	功能：隐藏平台
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
public class HiddenPlatform : MonoBehaviour
{
    private Tilemap _tileMap;
    private float _animTime = 1;
    private Tween _tween;
    private void Awake()
    {
        _tileMap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player))
            return;
        _tween?.Kill();
        _tween = DOTween.To(() => _tileMap.color, (value) => _tileMap.color = value, new Color(1, 1, 1, 0), _animTime)
            .SetEase(Ease.Linear)
            .SetUpdate(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player))
            return;
        _tween?.Kill();
        _tween = DOTween.To(() => _tileMap.color, (value) => _tileMap.color = value, new Color(1, 1, 1, 1), _animTime)
            .SetEase(Ease.Linear)
            .SetUpdate(true); 
    }
    private void OnDestroy()
    {
        _tween?.Kill();
    }
}