/****************************************************
    文件：Item.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/19 18:56:30
	功能：游戏中的物品数据
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
[InlineEditor]
[CreateAssetMenu(fileName = "newItemsData", menuName = "数据/物品信息/ItemsData")]
public class Items : ScriptableObject
{
    [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
    public List<Item> items;
}
public enum E_ItemTpye
{
    [LabelText("可使用的")]
    workable,//可使用的
    [LabelText("宝贵的")]
    precious,//宝贵的
    [LabelText("材料")]
    materials,//材料
}
[Serializable]
public class Item
{
    [HideLabel, PreviewField(100)]
    public Sprite sprite;
    [VerticalGroup("Item"), LabelText("物品名")]
    public string name;
    [VerticalGroup("Item"), LabelText("物品类型")]
    public E_ItemTpye tpye;
    [VerticalGroup("Item"), LabelText("物品价格")]
    public int price;
    [VerticalGroup("Item"), LabelText("物品信息"), MultiLineProperty(4)]
    public string tips;
}