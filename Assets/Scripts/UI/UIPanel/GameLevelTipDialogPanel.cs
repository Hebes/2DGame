/****************************************************
    文件：GameLevelTipPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/26 19:54:19
	功能：游戏关卡提示面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class GameLevelTipDialogPanel : PanelBase
{
    private CanvasGroup _canvasGroup;
    private float _tweenTime=2f;
    private float _waitTime=1f;
    public override void InitChild()
    {
        _canvasGroup = gameObject.AddOrGet<CanvasGroup>();
    }
    public void InitLevelTip(string levelName)
    {
        GetControl<Text>("txt_Title").SetText(levelName);
       
    }
    public async override void Show()
    {
        base.Show();
        await Task.Delay(TimeSpan.FromSeconds(_waitTime));
        _canvasGroup.DOFade(0, _tweenTime)
           .OnComplete(() => Hide())
           .SetUpdate(true)
           .SetEase(Ease.Linear);
        AudioMgr.Instance?.PlayOnce("hero_revive");
    }
    public override void Hide()
    {
        base.Hide();
        Destroy(gameObject);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        _canvasGroup.DOKill();
    }
}