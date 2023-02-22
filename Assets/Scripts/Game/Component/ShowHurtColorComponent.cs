using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShowHurtColorComponent : MonoBehaviour
{
    private SpriteRenderer _render;
    private float _showHitEffectTime = 0.2f;
    private Color _targetColor;
    public void Init(SpriteRenderer render,Color targetColor, float showHitEffectTime = 0.2f)
    {
        _render = render;
        _targetColor = targetColor;
        _showHitEffectTime = showHitEffectTime;
    }
    //显示受伤特效
    public void ShowHitEffect()
    {
        if (_render == null)
        {
            Debug.LogError($"该组件未初始化,或者初始化为成功");
            return;
        }
        _render.color = _targetColor;
        _render.DOKill();
        _render.DOColor(Color.white, _showHitEffectTime)
            .OnKill(() => _render.color = Color.white)
            .SetUpdate(true);//不受到时间缩放的影响
    }
    private void OnDestroy()
        => _render.DOKill();
}
