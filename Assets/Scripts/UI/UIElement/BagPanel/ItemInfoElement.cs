/****************************************************
    文件：ItemInfoElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/20 19:14:43
	功能：BagPanel下管理ItemInfo物品信息的集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInfoElement : UIElement
{
    public override void InitChild()
    {
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESITEMINFO,RefreshItemInfo);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESITEMINFO, RefreshItemInfo);
    }
    public override void Refresh()
    {
        base.Refresh();
        RefreshItemInfo();
    }
    private void RefreshItemInfo(params object[] args)
    {
        if (UIInfoModel.Instance.CurItemId == -1)
        {
            SetTextControlActive(false);
            return;
        }
        var itemInfo = UIInfoModel.Instance.GetCurSelectItemData();
        var num = GameDataModel.Instance.GetItemNum(UIInfoModel.Instance.CurItemId);
        GetControl<Text>("txt_Num").SetText(num);
        //\n被偷偷换成了\\n
        GetControl<Text>("txt_Tips").SetText(itemInfo.tips.Replace("\\n", "\n"));
        GetControl<Text>("txt_Name").SetText(itemInfo.name);
        SetTextControlActive(true);
    }
    private void SetTextControlActive(bool value)
    {
        GetControl<Text>("txt_Num").SetActive(value);
        GetControl<Text>("txt_Name").SetActive(value);
        GetControl<Text>("txt_Tips").SetActive(value);
        GetControl<Text>("txt_Msg").SetActive(value);
    }
}