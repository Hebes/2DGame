/****************************************************
    文件：ChooseGameArchivePanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/10 17:7:31
	功能：游戏存档选择Panel
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseGameArchivePanel : PanelBase
{
    public override void InitChild()
    {
        InitArchiveItemElements();
        AddBtnEvent();
        InitBtnEnterEvent();
        ArrowsUtil.CloseAllArrows();
    }
    private void Start()
    {
        ConvertButtonMusic("btn_Quit", E_UIMusic.NotClick);
    }
    private void InitArchiveItemElements()
    {
        for (int i = 0; i < 4; i++)
            transform.Find($"ArchiveItemElement{i}").Add<ArchiveItemElement>();
    }
    //添加按钮的动画事件
    private void AddBtnEvent()
    {
        GetControl<Button>("btn_Quit").onClick.AddListener(()=>
        {
            UIManager.Instance.HidePanel(Paths.PREFAB_UIPANEL_CHOOSEGAMEARCHIVEPANEL);
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_STARTPANEL);
        });
    }
    //初始化按钮进入事件
    private void InitBtnEnterEvent()
    {
        GetControl<Button>("btn_Quit").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(0);
        for (int i = 0; i < 4; i++)
        {
            int value = i;
            transform.Find($"ArchiveItemElement{value}/btn_Clear").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(value + 5);
            transform.Find($"ArchiveItemElement{value}/enterImg").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(value + 1);
            transform.Find($"ArchiveItemElement{value}/btn_SureClear").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(value*2 +9);
            transform.Find($"ArchiveItemElement{value}/btn_NotClear").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(value*2 + 10);
        }
    }
}