/****************************************************
    文件：AudioSettingElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/1 11:4:39
	功能：SettingPanel音量大小的调整模块
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingElement : UIElement
{
    private string _audioSettingTitle = "音量";
    private float _sldMaxValue = 1;
    public override void InitChild()
    {
        _sldMaxValue = GetControl<Slider>("sld_BgMusic").maxValue;
        AddBtnEvent();
        InitBtnEnterEvent();
    }
    private void Start()
    {
        ConvertButtonMusic("btn_Back", E_UIMusic.NotClick);
    }
    public override void Show()
    {
        base.Show();
        ArrowsUtil.CloseAllArrows();
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_SETTINGPLANE_CHANGETITLE, _audioSettingTitle);
    }
    public override void Refresh()
    {
        base.Refresh();
        RefreshSliderValue();
    }
    public override void Hide()
    {
        base.Hide();
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_SETTINGPLANE_CHANGETITLE);
        AudioMgr.SaveVolumeScale();//关闭面板的时候  进行数据缓存
    }
    private void AddBtnEvent()
    {
        //todo添加重置按钮的事件
        GetControl<Button>("btn_Reset").SetEvent(() => AudioMgr.ResetVolumeScale());
        GetControl<Button>("btn_Back").SetEvent(() => Hide());
        GetControl<Slider>("sld_BgMusic").onValueChanged.AddListener(UpdateBgMusicVolume);
        GetControl<Slider>("sld_SoundMusic").onValueChanged.AddListener(UpdateSoundMusicVolume);
        GetControl<Slider>("sld_GlobalMusic").onValueChanged.AddListener(UpdateGlobalMusicVolume);
    }
    private void InitBtnEnterEvent()
    {
        GetControl<Image>("img_Selected_0").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(0);
        GetControl<Image>("img_Selected_1").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(1);
        GetControl<Image>("img_Selected_2").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(4);
        GetControl<Button>("btn_Reset").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(2);
        GetControl<Button>("btn_Back").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(3);
    }
    private void RefreshSliderValue()
    {
        GetControl<Slider>("sld_BgMusic").SetValue(AudioMgr.BgVolumeScale * _sldMaxValue);
        GetControl<Slider>("sld_SoundMusic").SetValue(AudioMgr.SoundVolumeScale * _sldMaxValue);
        GetControl<Slider>("sld_GlobalMusic").SetValue(AudioMgr.GlobalVolumeScale * _sldMaxValue);
        GetControl<Text>("txt_BgMusic").SetText(AudioMgr.BgVolumeScale * _sldMaxValue);
        GetControl<Text>("txt_SoundMusic").SetText(AudioMgr.SoundVolumeScale * _sldMaxValue);
        GetControl<Text>("txt_GlobalMusic").SetText(AudioMgr.GlobalVolumeScale * _sldMaxValue);
    }
    private void UpdateBgMusicVolume(float value)
    {
        AudioMgr.BgVolumeScale = value / _sldMaxValue;
        GetControl<Text>("txt_BgMusic").SetText(AudioMgr.BgVolumeScale * _sldMaxValue);
    }
    private void UpdateSoundMusicVolume(float value)
    {
        AudioMgr.SoundVolumeScale = value / _sldMaxValue;
        GetControl<Text>("txt_SoundMusic").SetText(AudioMgr.SoundVolumeScale * _sldMaxValue);
    }
    private void UpdateGlobalMusicVolume(float value)
    {
        AudioMgr.GlobalVolumeScale = value / _sldMaxValue;
        GetControl<Text>("txt_GlobalMusic").SetText(AudioMgr.GlobalVolumeScale * _sldMaxValue);
    }
}