/****************************************************
    文件：SelectionsElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/19 13:56:24
	功能：ChatPanel下面管理SelectionItemElement的集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SelectionsElement : UIElement
{
    private List<SelectionItemElement> _itemsList = new List<SelectionItemElement>();
    private ChatModel _curChatModel;
    public override void InitChild()
    {

    }
    public override void Refresh()
    {
        base.Refresh();
        CreateSelectionItems();
    }
    private void CreateSelectionItems()
    {
        //生成现在的
        if (UIInfoModel.Instance.CurNpcIndex == -1)
        {
            ClearLastItems();
            _curChatModel = null;
            return;
        }
        var model = UIInfoModel.Instance.GetCurChatModel();
        if (_curChatModel == null||_curChatModel!=model)
        {
            ClearLastItems();
            _curChatModel = model;
            for (int i = 0; i < model.dialogPlayerSelects.Count; i++)
            {
                var item = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIELEMENT_SELECTIONITEM, transform)
                    .AddComponent<SelectionItemElement>();
                item.Init(model.dialogPlayerSelects[i]);
                _itemsList.Add(item);
            }
            Reacquire();
        }
    }
    private void ClearLastItems()
    {
        //清除之前的
        foreach (var item in _itemsList)
            Destroy(item.gameObject);
        _itemsList.Clear();
    }
}