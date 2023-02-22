/****************************************************
    文件：LoadingPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/27 21:2:19
	功能：游戏加载面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : PanelBase
{
    //进行帧率限制
    private bool _isLoadingScene = false;
    private int _timer;
    private int _updateTimes = 5;

    private bool _needShowGameLevelTipsPanel=false;
    public override void InitChild()
    {
        transform.Find("GameTipsElement").Add<GameTipsElement>();
        transform.Find("ProgressMsgElement").Add<ProgressMsgElement>();
    }
    public override void Show()
    {
        _needShowGameLevelTipsPanel = false;
        _isLoadingScene = false;
        if (CheckCanChanegScene())
        {
            SceneMgr.Instance.LoadSceneAsyn(GameStateModel.Instance.TargetScene);
            _isLoadingScene = true;
        }
        UpdateBg(UIInfoModel.Instance.GetLoadingSprite());
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL,false);
        TimeScaleMgr.Instance.StopGame();
        AudioMgr.Instance.PauseAllAudio();
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL, true);
        if (_needShowGameLevelTipsPanel)
            DialogMgr.ShowGameLevelTipDialogPanel(UIInfoModel.Instance.GetCurLevelName());
        TimeScaleMgr.Instance.ContinueGame();
    }
    //更新加载面板的背景图片
    private void UpdateBg(Sprite bg)
        => GetControl<Image>("img_bg").SetImage(bg);
    //检测当前是不是可以切换场景
    private bool CheckCanChanegScene()
    {
        if (GameStateModel.Instance.TargetScene != E_SceneName.None)
            return true;
        Debug.LogError($"切换场景失败,当前场景为:{GameStateModel.Instance.CurScene},目标场景为:{GameStateModel.Instance.TargetScene}");
        return false;
    }
    private void Update()
    {
        if (!_isLoadingScene || !GetActive())
            return;
        if (_timer < _updateTimes)
        {
            _timer++;
            return;
        }
        _timer = 0;
        Refresh();
    }
    public override void Refresh()
    {
        base.Refresh();
        if (SceneMgr.Instance.Process() == 1f)
        {
            AudioMgr.Instance.ContinueAllAudio();
            //跟目标场景来打开相关面板
            //todo显示目标场景的面板
            //todo关闭当前面板
            switch (GameStateModel.Instance.TargetScene)
            {
                case E_SceneName.StartScene:
                    UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_STARTPANEL);
                    AudioMgr.Instance.PlayBgMusic(E_BgMusic.bgmusic_startScene);
                    break;
                case E_SceneName.LevelOne:
                    AudioMgr.Instance.PlayBgMusic(E_BgMusic.bgmusic_levelOne);
                    UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_GAMEPANEL);
                    _needShowGameLevelTipsPanel = true;
                    break; 
                case E_SceneName.LevelTwo:
                    AudioMgr.Instance.PlayBgMusic(E_BgMusic.bgmusic_levelTwo);
                    UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_GAMEPANEL);
                    _needShowGameLevelTipsPanel = true;
                    break;
            }
            _isLoadingScene = false;
            UIManager.Instance.HidePanel(Paths.PREFAB_UIPANEL_LOADINGPANEL);
        }
    }
    private void OnDisable()
    {
        if (GameStateModel.Instance.TargetScene != E_SceneName.None)
            GameStateModel.Instance.TargetScene = E_SceneName.None;
    }
}