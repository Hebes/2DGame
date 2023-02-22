/****************************************************
    文件：ShopPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/22 19:34:33
	功能：商店面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : PanelBase
{
    private Transform _contentTf;
    private int _curToggle = -1;
    public List<GameObject> _itemList = new List<GameObject>();
    public override void InitChild()
    {
        transform.Find("ItemInfoElement").Add<ItemInfoElement>();
        _contentTf = GetControl<ScrollRect>("sv_Bag").content;
        InitControlEvent();
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESCOIN,RefreshCoin);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESCOIN, RefreshCoin);
    }
    //初始化UI事件
    private void InitControlEvent()
    {
        GetControl<Button>("btn_Quit").SetEvent(UIManager.Instance.BackPanel);
        GetControl<Toggle>("tgl_Workable").onValueChanged.AddListener((value) =>
        {
            if (value)
                CreateItems(0);
        });
        GetControl<Toggle>("tgl_Precious").onValueChanged.AddListener((value) =>
        {
            if (value)
                CreateItems(1);
        });
        GetControl<Toggle>("tgl_Materials").onValueChanged.AddListener((value) =>
        {
            if (value)
                CreateItems(2);
        });
    }
    public override void Show()
    {
        _curToggle = -1;
        UIInfoModel.Instance.CurItemId = -1;
        GetControl<Toggle>("tgl_Workable").isOn = false;
        GetControl<Toggle>("tgl_Precious").isOn = false;
        GetControl<Toggle>("tgl_Materials").isOn = false;
        GetControl<Toggle>("tgl_Workable").isOn=true;
        SetCoinNum(GameDataModel.Instance.GetCurCoin());
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
        UIInfoModel.Instance.CurShopId = -1;
    }
    private void CreateItems(int toggle)
    {
        if (_curToggle == toggle)
            return;
        foreach (var item in _itemList)
            Destroy(item.gameObject);
        _itemList.Clear();
        OneShopDatas shopInfos=UIInfoModel.Instance.GetCurSelectedShopDatas();
        List<ShopData> shopDataList = new List<ShopData>();
        E_ItemTpye curItemType=E_ItemTpye.workable;
        switch (toggle)
        {
            case 0:
                GetControl<Text>("txt_CurSelectName").SetText("使用道具");
                break;
            case 1:
                GetControl<Text>("txt_CurSelectName").SetText("贵重物品");
                curItemType = E_ItemTpye.precious;
                break;
            case 2:
                GetControl<Text>("txt_CurSelectName").SetText("素材道具");
                curItemType = E_ItemTpye.materials;
                break;
        }
        foreach (var shop in shopInfos.shopDatas)
        {
            var itemData = UIInfoModel.Instance.GetItemData(shop.id);
            if (itemData.tpye == curItemType)
                shopDataList.Add(shop);
        }
        for (int i = 0; i < shopDataList.Count; i++)
        {
            var curShopInfo = shopDataList[i];
            //设置当前的第一个物品 为选择 物品 更新信息面板的显示
            if (i == 0)
                UIInfoModel.Instance.CurItemId = curShopInfo.id;
            var itemGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIELEMENT_ITEMELEMENT, _contentTf);
            var shopItem = itemGo.AddOrGet<ShopItemElement>();
            shopItem.Init(curShopInfo);
            _itemList.Add(itemGo);
        }
        if (shopDataList.Count == 0)
            UIInfoModel.Instance.CurItemId = -1;
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESITEMINFO);
        _curToggle = toggle;
    }
    private void RefreshCoin(params object[] args)
        => SetCoinNum(GameDataModel.Instance.GetCurCoin());//设置当前金币的数量
    private void SetCoinNum(int coin)
      => GetControl<Text>("txt_CoinNum").SetText(coin);
}