/****************************************************
    文件：ArchivePointPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/12 13:24:28
	功能：游戏中的篝火面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchivePointPanel : PanelBase
{
    private LookBackElement _lookBaclElement;
    public override void InitChild()
    {
        _lookBaclElement = transform.Find("LookBackElement").Add<LookBackElement>();
        InitBtnClickEvent();
        InitEnterEvent();
        ArrowsUtil.CloseAllArrows();
    }
    private void Start()
    {
        ConvertButtonMusic("btn_Quit", E_UIMusic.NotClick);
    }
    public override void Show()
    {
        base.Show();
        _lookBaclElement.Hide();
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL, false);
    }
    public override void Hide()
    {
        base.Hide();
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL, true);
    }
    private void InitBtnClickEvent()
    {
        GetControl<Button>("btn_Rest").SetEvent(() =>
            {
                EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_REST);
                UIManager.Instance.BackPanel();
            });
        GetControl<Button>("btn_LookBack").SetEvent(() => _lookBaclElement.Show());
        GetControl<Button>("btn_Quit").SetEvent(() => UIManager.Instance.BackPanel());
    }
    private void InitEnterEvent()
    {
        GetControl<Button>("btn_Rest").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(0);
        GetControl<Button>("btn_LookBack").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(1);
        GetControl<Button>("btn_Quit").AddOrGet<UIEventUtil>().OnEnterEvent += (data) => ArrowsUtil.ChangeActiveArrows(2);
    }
}
