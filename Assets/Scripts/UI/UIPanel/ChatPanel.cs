/****************************************************
    文件：ChatPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/18 21:37:5
	功能：对话面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ChatPanel : PanelBase
{
    private float _tweenTime = 1f;//动画持续时间
    public override void InitChild()
    {
        transform.Find("SelectionsElement").AddOrGet<SelectionsElement>();
        EventMgr.Instance.AddEvent(E_EventName.CHAT_OVER, ChatOver);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventMgr.Instance.RemoveEvent(E_EventName.CHAT_OVER, ChatOver);
    }
    public override void Show()
    {
        base.Show();
        UIInfoModel.Instance.CurNpcModelIndex = 0;
        RefreshName(UIInfoModel.Instance.GetCurNpcName());
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL, false);
    }
    public override void Hide()
    {
        base.Hide();
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL, true);
    }
    public override void Refresh()
    {
        base.Refresh();
        if (UIInfoModel.Instance.CurNpcIndex == -1)
            return;
        DoCurNpcAction();
        RefreshContent(UIInfoModel.Instance.GetCurChatModel().npcContent);
    }
    //刷新对话内容
    private void RefreshContent(string content)
    {
        var text = GetControl<Text>("txt_Content");
        text.SetText("");
        text.DOKill();
        text.DOText(content, _tweenTime)
            .SetEase(Ease.Linear);
    }
    //刷新名字显示
    private void RefreshName(string name)
        => GetControl<Text>("txt_Name").SetText(name);
    //完成当前NPC对话对应的事件
    private void DoCurNpcAction()
        => UIInfoModel.Instance.ExcuteCurNpcModelEvent();
    private void ChatOver(params object[] args)
        => UIManager.Instance.BackPanel();
}