/****************************************************
    文件：StartPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/27 21:2:36
	功能：游戏开始面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StartPanel : PanelBase
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
    }
    //添加按钮点击事件
    private void AddBtnEvent()
    {
        GetControl<Button>("btn_StartGame").onClick.AddListener(() => UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_CHOOSEGAMEARCHIVEPANEL));
        GetControl<Button>("btn_About").onClick.AddListener(() => UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_ABOUTPANEL));
        GetControl<Button>("btn_GameSetting").onClick.AddListener(() => UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_SETTINGPANEL));
        GetControl<Button>("btn_Quit").onClick.AddListener(() => DialogMgr.ShowDialog("提示", "确认退出?", QuitGame, () => { }));
        GetControl<Button>("btn_Achievement").onClick.AddListener(() => DialogMgr.ShowDialog("致歉", "开发中","确定","否定"));
    }
    private void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private void InitBtnEnterEvent()
    {
        GetControl<Button>("btn_StartGame").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(0);
        GetControl<Button>("btn_GameSetting").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(1);
        GetControl<Button>("btn_About").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(2);
        GetControl<Button>("btn_Achievement").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(3);
        GetControl<Button>("btn_Quit").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(4);
    }
}