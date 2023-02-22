/****************************************************
    文件：ShopDatas.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/19 21:18:30
	功能: 商店信息数据
*****************************************************/
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "newShopDatas", menuName = "数据/商店信息/ShopDatas")]
public class ShopDatas : ScriptableObject
{
    [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true), HideLabel]
    public List<OneShopDatas> shopsData;
}
[Serializable]
public class OneShopDatas
{
    public List<ShopData> shopDatas;
}
[Serializable]
public class ShopData
{
    [VerticalGroup("Item"), LabelText("物品id")]
    public int id;//对应的物品id
    [VerticalGroup("Item"), LabelText("物品一次性购买的数量")]
    public int num;//一次性购买的数量
}