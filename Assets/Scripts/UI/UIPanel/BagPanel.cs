/****************************************************
    文件：BagPanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/20 15:56:8
	功能：游戏中的背包面板
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagPanel : PanelBase
{
    private Transform _contentTf;
    private int _curToggle = -1;
    public List<GameObject> _itemList = new List<GameObject>();
    public override void InitChild()
    {
        transform.Find("ItemInfoElement").Add<ItemInfoElement>();
        _contentTf = GetControl<ScrollRect>("sv_Bag").content;
        InitControlEvent();
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
        GetControl<Toggle>("tgl_Workable").isOn = true;
        SetCoinNum(GameDataModel.Instance.GetCurCoin());//设置当前金币的数量
        RefreshHeroMsg();
        base.Show();
    }
    private void CreateItems(int toggle)
    {
        if (_curToggle == toggle)
            return;
        foreach (var item in _itemList)
            Destroy(item.gameObject);
        _itemList.Clear();
        switch (toggle)
        {
            case 0:
                GetControl<Text>("txt_CurSelectName").SetText("使用道具");
                break;
            case 1:
                GetControl<Text>("txt_CurSelectName").SetText("贵重物品");
                break;
            case 2:
                GetControl<Text>("txt_CurSelectName").SetText("素材道具");
                break;
        }
        var itemInfos = GameDataModel.Instance.GetPlayerContainItemInfos(toggle);
        for (int i = 0; i < itemInfos.Count; i++)
        {
            //设置当前的第一个物品 为选择 物品 更新信息面板的显示
            if (i == 0)
                UIInfoModel.Instance.CurItemId = itemInfos[i].id;
            var itemGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIELEMENT_ITEMELEMENT, _contentTf);
            int num = GameDataModel.Instance.GetItemNum(itemInfos[i].id);
            var itemData = UIInfoModel.Instance.GetItemData(itemInfos[i].id);
            itemGo.AddOrGet<ItemElement>().Init(itemData, num);
            _itemList.Add(itemGo);
        }
        if (itemInfos.Count == 0)
            UIInfoModel.Instance.CurItemId = -1;
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESITEMINFO);
        _curToggle = toggle;
    }
    private void SetCoinNum(int coin)
       => GetControl<Text>("txt_CoinNum").SetText(coin);
    private void RefreshHeroMsg()
    {
        var setting = GameDataModel.Instance.GetHeroManSettingData;
        var msg =
                    $"攻击力：{setting.attack.ToString().SetColor(E_TextColor.Red)}\t\t" +
                    $"生    命：{setting.maxHealth.ToString().SetColor(E_TextColor.Green)}\n" +
                    $"恢复力：{setting.lightAddRatio.ToString().SetColor(E_TextColor.Yellow)}\t\t" +
                    $"魔法值：{setting.maxMagicPower.ToString().SetColor(E_TextColor.Blue)}";
        GetControl<Text>("txt_HeroMsg").SetText(msg);
    }
}