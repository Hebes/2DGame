/****************************************************
    文件：GameSettingPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/16 15:57:53
	功能：游戏中的设置面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSettingPanel : PanelBase
{
    public override void InitChild()
    {
        AddBtnEvent();
        InitBtnEnterEvent();
    }
    public override void Show()
    {
        base.Show();
        ArrowsUtil.CloseAllArrows();
        TimeScaleMgr.Instance.StopGame();
        AudioMgr.Instance.PauseAllAudio();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public override void Hide()
    {
        base.Hide();
        TimeScaleMgr.Instance.ContinueGame();
        AudioMgr.Instance.ContinueAllAudio();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //添加按钮点击事件
    private void AddBtnEvent()
    {
        GetControl<Button>("btn_Continue").onClick.AddListener(() => UIManager.Instance.BackPanel());
        GetControl<Button>("btn_Bag").onClick.AddListener(() => UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_BAGPANEL));
        GetControl<Button>("btn_GameSetting").onClick.AddListener(() =>  UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_SETTINGPANEL));
        GetControl<Button>("btn_BackMenu").onClick.AddListener(()=>
        {
            DialogMgr.ShowDialog("确认退出到菜单?", "游戏进度将自动保存", () =>
            {
                GameStateModel.Instance.TargetScene = E_SceneName.StartScene;
                UIManager.Instance.BackPanel();
                UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_LOADINGPANEL);
            }, () => { });
        });
    }
    private void InitBtnEnterEvent()
    {
        GetControl<Button>("btn_Continue").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(0);
        GetControl<Button>("btn_GameSetting").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(1);
        GetControl<Button>("btn_Bag").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(2);
        GetControl<Button>("btn_BackMenu").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(3);
    }
}