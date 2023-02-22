/****************************************************
    文件：GameEndPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/16 14:50:43
	功能：游戏结束面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class GameEndPanel : PanelBase
{
    private float _loseAlpha = 1f;
    private float _winAlpha = 0.2f;
    private Image _imgBg;
    public override void InitChild()
    {
        _imgBg = GetComponent<Image>();
    }
    public override void Show()
    {
        base.Show();
        RefreshImage();
        PlaySound();
        Invoke("DelayFunc", 2f);
    }
    private void DelayFunc()
    {
        UIManager.Instance.HidePanel(Paths.PREFAB_UIPANEL_GAMEENDPANEL);
    }
    public override void Hide()
    {
        base.Hide();
        HideAction();
        GameStateModel.Instance.CurGameState = E_GameState.NONE;
    }
    private void HideAction()
    {
        if (GameStateModel.Instance.CurGameState == E_GameState.Lose)
        {
            GameStateModel.Instance.TargetScene = (E_SceneName)(Enum.Parse(typeof(E_SceneName), SceneManager.GetActiveScene().name));
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_LOADINGPANEL);
        }
    }
    private void RefreshImage()
    {
        var color = _imgBg.color;
        color.a = GameStateModel.Instance.CurGameState == E_GameState.Lose ? _loseAlpha : _winAlpha;
        _imgBg.color = color;
        GetControl<Image>("img_Tip").SetImage(UIInfoModel.Instance.GetGameEndPanelSprite(GameStateModel.Instance.CurGameState));
    }
    private void PlaySound()
    {
        switch (GameStateModel.Instance.CurGameState)
        {
            case E_GameState.Lose:
                AudioMgr.Instance.PlayOnce("hero_dead");
                break;
            case E_GameState.Win:
                AudioMgr.Instance.PlayOnce("hero_killBoss");
                PlayBgMusic();
                break;
            case E_GameState.Pause:
                AudioMgr.Instance.PlayOnce("hero_unlockArchivePoint");
                break;
        }
    }
    private void PlayBgMusic()
        => AudioMgr.Instance.PlayBgMusic(E_BgMusic.bgmusic_gameEnd);
}