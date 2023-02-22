/****************************************************
    文件：UVTransformMove.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/27 21:47:53
	功能：UI偏移组件
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UVTransformMove : MonoBehaviour
{
    //uv偏移的速度
    private Vector2 _uvAnimationRate;
    //当前uv偏移的程度
    private Vector2 _uvOffset = Vector2.zero;
    private SpriteRenderer _render;

    public void UpdateMove(float ratio=1)
    {
        MoveBackGround(ratio);
    }
    //初始化UV偏移
    public void InitUVTrans(Vector2 uvAnimRate,float playerFacingDir)
    {
        _uvAnimationRate = uvAnimRate;
        _uvAnimationRate.y = Mathf.Abs(_uvAnimationRate.y);
        _uvAnimationRate.x = playerFacingDir>0?Mathf.Abs(_uvAnimationRate.x): -Mathf.Abs(_uvAnimationRate.x);
        _render = GetComponent<SpriteRenderer>();
        if(_render==null)
            Debug.LogError($"当前物体:{name}身上未挂载SpriteRener组件");
    }
    //在Update中调用
    private void MoveBackGround(float ratio)
    {
        _uvOffset += _uvAnimationRate * Time.deltaTime * ratio;
        if (_render.material != null)
            _render.material.mainTextureOffset = _uvOffset;
    }
}