/****************************************************
    文件：KeyCodeSettingElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/1 11:4:57
	功能：SettinPanel的游戏键位调整模块
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCodeSettingElement : UIElement
{
    private string _keyCodeSettingTitle = "键位";
    public override void InitChild()
    {
        AddBtnEvent();
        InitBtnEnterEvent();
        InitSettingItems();
    }
    private void Start()
    {
        ConvertButtonMusic("btn_Back", E_UIMusic.NotClick);
    }
    public override void Show()
    {
        base.Show();
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_SETTINGPLANE_CHANGETITLE, _keyCodeSettingTitle);
        ArrowsUtil.CloseAllArrows();
    }
    public override void Hide()
    {
        base.Hide();
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_SETTINGPLANE_CHANGETITLE);
    }
    private void AddBtnEvent()
    {
        GetControl<Button>("btn_Back").onClick.AddListener(() => Hide());
        GetControl<Button>("btn_Reset").onClick.AddListener(() => InputMgr.Instance.ResetInputSetting());
    }
    private void InitBtnEnterEvent()
    {
        GetControl<Button>("btn_Back").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(0);
        GetControl<Button>("btn_Reset").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(1);
        for (int i = 0; i < 12; i++)
        {
            int value = i;
            transform.Find($"ItemSettingElement{value}").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(value + 2);
        }
    }
    private void InitSettingItems()
    {
        transform.Find("ItemSettingElement0").Add<ItemSettingElement>().Init(E_KeyType.AxisPosKey, Consts.VERTICALAXIS);
        transform.Find("ItemSettingElement1").Add<ItemSettingElement>().Init(E_KeyType.AxisNegKey, Consts.VERTICALAXIS);
        transform.Find("ItemSettingElement2").Add<ItemSettingElement>().Init(E_KeyType.AxisNegKey, Consts.HORIZONTALAXIS);
        transform.Find("ItemSettingElement3").Add<ItemSettingElement>().Init(E_KeyType.AxisPosKey, Consts.HORIZONTALAXIS);
        transform.Find("ItemSettingElement4").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.ATTACKEY);
        transform.Find("ItemSettingElement5").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.JUMPKEY);
        transform.Find("ItemSettingElement6").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.BOWKEY);
        transform.Find("ItemSettingElement7").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.GRABKEY);
        transform.Find("ItemSettingElement8").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.DASHKEY);
        transform.Find("ItemSettingElement9").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.CHANGEITEM);
        transform.Find("ItemSettingElement10").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.USEITEM);
        transform.Find("ItemSettingElement11").Add<ItemSettingElement>().Init(E_KeyType.Key, Consts.ATTRACTCOIN);
    }
}