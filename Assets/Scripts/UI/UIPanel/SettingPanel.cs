/****************************************************
    文件：SettingPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/1 11:28:48
	功能：游戏设置面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : PanelBase
{
    private AudioSettingElement _audioSettingElement;
    private KeyCodeSettingElement _keyCodeSettingElement;
    private string _settingTitle = "选项";
    public override void InitChild()
    {
        var audioSettingTf = transform.Find("AudioSettingElement");
        var keyCodeSettingTf = transform.Find("KeyCodeSettingElement");
        _audioSettingElement = audioSettingTf.Add<AudioSettingElement>();
        _keyCodeSettingElement = keyCodeSettingTf.Add<KeyCodeSettingElement>();
        AddBtnEvent();
        InitBtnEnterEvent();
        EventMgr.Instance.AddEvent(E_EventName.UI_SETTINGPLANE_CHANGETITLE, ChangeTitle);
    }
    private void Start()
    {
        ConvertButtonMusic("btn_Quit", E_UIMusic.NotClick);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventMgr.Instance.RemoveEvent(E_EventName.UI_SETTINGPLANE_CHANGETITLE, ChangeTitle);
    }
    public override void Show()
    {
        base.Show();
        ArrowsUtil.CloseAllArrows();
        _audioSettingElement.Hide();
        _keyCodeSettingElement.Hide();
    }
    private void AddBtnEvent()
    {
        GetControl<Button>("btn_AudioSetting").onClick.AddListener(() => _audioSettingElement.Show());
        GetControl<Button>("btn_KeyCodeSetting").onClick.AddListener(() => _keyCodeSettingElement.Show());
        GetControl<Button>("btn_Quit").onClick.AddListener(() => UIManager.Instance.BackPanel());
    }
    private void InitBtnEnterEvent()
    {
        GetControl<Button>("btn_AudioSetting").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(0);
        GetControl<Button>("btn_KeyCodeSetting").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(1);
        GetControl<Button>("btn_Quit").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(2);
    }
    private void ChangeTitle(params object[] args)
    {
        string title = "";
        if (args == null || args.Length == 0)
            title = _settingTitle;
        else
            title = args[0] as string;
        GetControl<Text>("txt_Title").SetText(title);
    }
}