/****************************************************
    文件：GameBlackPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/27 17:25:1
	功能：黑屏面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class GameBlackPanel : PanelBase
{
    private float _awaitTime=0.8f;
    private Image _img;
    public override void InitChild()
    {
        _img = GetComponent<Image>();
    }
    public async override void Show()
    {
        gameObject.SetActive(true);
        TimeScaleMgr.Instance.StopGame();
        await Task.Delay(TimeSpan.FromSeconds(_awaitTime));
        TimeScaleMgr.Instance.ContinueGame();
        UIManager.Instance.HidePanel(Paths.PREFAB_UIPANEL_GAMEBLACKPANEL);
    }
    public override void Hide()
    {
        gameObject?.SetActive(false);
    }
}