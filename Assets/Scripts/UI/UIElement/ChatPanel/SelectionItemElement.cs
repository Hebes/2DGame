/****************************************************
    文件：SelectionItemElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/19 13:57:23
	功能：管理单个选项的
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionItemElement : MonoBehaviour
{
    private UIUtil _uitl;
    private void InitEnterEvent()
    {
        _uitl.GetControl<Button>("btn_Select").AddOrGet<UIEventUtil>().OnEnterEvent += (data) =>
        {
            foreach (Transform item in _uitl.GetControl<Text>("txt_Select").transform)
                item.SetActive(true);
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.Enter);
        };
        _uitl.GetControl<Button>("btn_Select").AddOrGet<UIEventUtil>().OnExitEvent += (data) =>
        {
            foreach (Transform item in _uitl.GetControl<Text>("txt_Select").transform)
                item.SetActive(false);
        };
    }
    public void Init(ChatPlayerSelect playerSelect)
    {
        _uitl = gameObject.AddOrGet<UIUtil>();
        _uitl.Init();
        _uitl.GetControl<Text>("txt_Select").SetText(playerSelect.selectionName);
        _uitl.GetControl<Button>("btn_Select").SetEvent(() => UIInfoModel.Instance.ExcuteSelectPlayerModelEvent(playerSelect));
        InitEnterEvent();
    }
}