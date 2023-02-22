/****************************************************
    文件：ItemSettingElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/3 18:39:29
	功能：键位设置单位
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSettingElement : UIElement
{
    private Action<KeyCode> SetKey;
    private string tipString = "设定键位";


    private E_KeyType keyType;
    private string keyName;//键位名称
    public override void InitChild()
    {
        gameObject.AddOrGet<UIEventUtil>().OnClickEvent += ItemSettingElement_OnClickEvent;
        EventMgr.Instance.AddEvent(E_EventName.INPUTSYS_SETKEYNONE, SetKeyCodeNONE);
        EventMgr.Instance.AddEvent(E_EventName.INPUTSYS_CLOSEOTHERSETTINGKEY, CloseNotSettingItem);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventMgr.Instance.RemoveEvent(E_EventName.INPUTSYS_SETKEYNONE, SetKeyCodeNONE);
        EventMgr.Instance.RemoveEvent(E_EventName.INPUTSYS_CLOSEOTHERSETTINGKEY, CloseNotSettingItem);
    }
    public void Init(E_KeyType keyType, string keyName)
    {
        this.keyType = keyType;
        this.keyName = keyName;
    }
    public override void Show()
    {
        base.Show();
        GetControl<Image>("img_Box").SetActive(true);
    }
    private void ItemSettingElement_OnClickEvent(PointerEventData data)
    {
        GetControl<Text>("txt_KeyCodeName").SetText(tipString);
        GetControl<Image>("img_Box").SetActive(false);
        switch (keyType)
        {
            case E_KeyType.Key:
                SetKey = (key) => InputMgr.Instance.SetKey(keyName, key);
                break;
            case E_KeyType.ValueKey:
                SetKey = (key) => InputMgr.Instance.SetValueKey(keyName, key);
                break;
            case E_KeyType.AxisPosKey:
                SetKey = (key) => InputMgr.Instance.SetAxisPosKey(keyName, key);
                break;
            case E_KeyType.AxisNegKey:
                SetKey = (key) => InputMgr.Instance.SetAxisNegKey(keyName, key);
                break;
        }
        //设置成功才会执行对应的委托
        InputMgr.Instance.StartSetKey(SetKey, SetText);
        var name = keyType + "|" + keyName;
        EventMgr.Instance.ExecuteEvent(E_EventName.INPUTSYS_CLOSEOTHERSETTINGKEY, name);
    }
    public override void Refresh()
    {
        base.Refresh();
        RefreshKeyCode();
    }
    private void RefreshKeyCode()
    {
        switch (keyType)
        {
            case E_KeyType.Key:
                SetText(InputMgr.Instance.GetKeyCode(keyName));
                break;
            case E_KeyType.ValueKey:
                SetText(InputMgr.Instance.GetValueKey(keyName));
                break;
            case E_KeyType.AxisPosKey:
                SetText(InputMgr.Instance.GetAxisPosKey(keyName));
                break;
            case E_KeyType.AxisNegKey:
                SetText(InputMgr.Instance.GetAxisNegKey(keyName));
                break;
        }
    }
    public void SetText(KeyCode keyCode)
    {
        GetControl<Text>("txt_KeyCodeName").SetText(keyCode.ToString());
        GetControl<Image>("img_Box").SetActive(true);
    }
    //置空当前键位
    private void SetKeyCodeNONE(params object[] args)
    {
        var tempName = args[0] as string;
        var strs = tempName.Split('|');
        if (keyType != (E_KeyType)Enum.Parse(typeof(E_KeyType), strs[0]) || keyName != strs[1])
            return;
        RefreshKeyCode();
    }
    private void CloseNotSettingItem(params object[] args)
    {
        var tempName = args[0] as string;
        var strs = tempName.Split('|');
        if (keyType != (E_KeyType)Enum.Parse(typeof(E_KeyType), strs[0]) || keyName != strs[1])
            RefreshKeyCode();
    }
}