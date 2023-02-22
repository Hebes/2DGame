/****************************************************
    文件：Door.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/18 18:52:11
	功能：门
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Door : MonoBehaviour
{
    private Collider2D _collider;
    private SpriteRenderer _render;
    private float _openHeight = 1f;
    private float _closeHeight = 5f;
    private Tween _curTween;
    public void Init()
    {
        InitComponent();
        SetCollider(false);
    }
    private void InitComponent()
    {
        _collider = GetComponent<Collider2D>();
        _render = GetComponent<SpriteRenderer>();
    }
    public void CloseOrOpenDoorDoor(bool open)
    {
        var targetSize = _render.size;
        if (open)
        {
            SetCollider(false);
            targetSize.y = _openHeight;
        }
        else
        {
            SetCollider(true);
            targetSize.y = _closeHeight;
        }
        _curTween?.Kill();
        _curTween = DOTween.To(() => _render.size, (value) => _render.size = value, targetSize, 0.5f)
            .SetEase(Ease.InExpo);
    }
    private void SetCollider(bool value)
        => _collider.enabled = value;
}