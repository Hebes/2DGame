/****************************************************
    文件：GetItemDialogPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/24 16:35:45
	功能：得到物品提示面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GetItemDialogPanel : PanelBase
{
    public void InitGetItemDialog(int itemId,int num,Action callback=null)
    {
        var itemData = UIInfoModel.Instance.GetItemData(itemId);
        var name = itemData.name;
        var sprite = itemData.sprite;
        var tips = itemData.tips.Replace("\\n","\n");
        GetControl<Text>("txt_Name").SetText(name);
        GetControl<Text>("txt_Tips").SetText(tips);
        GetControl<Text>("txt_Num").SetText(num);
        if(num<=1)
            GetControl<Text>("txt_Num").SetActive(false);
        GetControl<Image>("img_Icon").SetImage(sprite);
        InitBtnEvent(callback);
    }
    public override void Show()
    {
        base.Show();
        TimeScaleMgr.Instance.StopGame();
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL,false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public override void InitChild()
    {
        InitBtnEnterEvent();
    }
    private void InitBtnEnterEvent()
    {
        GetControl<Button>("btn_Sure").AddOrGet<UIEventUtil>().OnEnterEvent += (data) =>
        {
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowOne").SetActive(true);
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowTwo").SetActive(true);
        };
        GetControl<Button>("btn_Sure").AddOrGet<UIEventUtil>().OnExitEvent += (data) =>
        {
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowOne").SetActive(false);
            GetControl<Button>("btn_Sure").transform.Find("txt_SureText/arrowTwo").SetActive(false);
        };
    }
    private void InitBtnEvent(Action callback)
    {
        GetControl<Button>("btn_Sure").onClick.AddListener(()=>callback?.Invoke());
        GetControl<Button>("btn_Sure").onClick.AddListener(Hide);
    }
    public override void Hide()
    {
        base.Hide();
        TimeScaleMgr.Instance.ContinueGame();
        Destroy(gameObject);
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL, true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}