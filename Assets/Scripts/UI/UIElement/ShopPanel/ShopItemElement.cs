/****************************************************
    文件：ShopItemElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/22 19:37:41
	功能：ShopPanel下的单个物品的合集
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemElement : MonoBehaviour
{
    public int _id;
    private UIUtil _uitil;
    private int _price;//当前价格
    private ShopData _shopData;
    public void Init(ShopData shopData)
    {
        _shopData = shopData;
        var data = UIInfoModel.Instance.GetItemData(shopData.id);
        _uitil = gameObject.AddOrGet<UIUtil>();
        _uitil.Init();
        this._id = UIInfoModel.Instance.GetItemId(data);
        _price = data.price;
        RefreshShow(data);
        InitEnterEvent();
        SetArrowActive(false);
        SetBtnActive(false);
        SetEquipActive(false);
        InitClickEvent(data);
        ShowPriceInfo();
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESSHOPITEMTEXT,RefreshTextColor);
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESSHOPITEMTEXT, RefreshTextColor);
    }
    private void RefreshShow(Item data)
    {
        _uitil.GetControl<Image>("img_Icon").SetImage(data.sprite);
        _uitil.GetControl<Text>("txt_Name").SetText(data.name);
        _uitil.GetControl<Text>("txt_Num").SetActive(false);
    }
    private void InitEnterEvent()
    {
        _uitil.GetControl<Image>("img_bk").AddOrGet<UIEventUtil>().OnEnterEvent += ((data) =>
        {
            SetArrowActive(true);
            UIInfoModel.Instance.CurItemId = _id;
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESITEMINFO);
        });
        _uitil.GetControl<Image>("img_bk").AddOrGet<UIEventUtil>().OnExitEvent += ((data) => SetArrowActive(false));
    }
    private void InitClickEvent(Item data)
    {
        _uitil.GetControl<Image>("img_bk").AddOrGet<UIEventUtil>().OnClickEvent += ((eventData) =>
        {
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.NormalClick);
            //如果金币不够
            if (GameDataModel.Instance.GetCurCoin() < _price)
                DialogMgr.ShowDialog("购买提示","金钱不足".SetColor(E_TextColor.Red),"确定","取消");
            else
            {
                DialogMgr.ShowDialog("购买提示", "确认购买?", "确定", "取消",()=>
                {
                    if(GameDataModel.Instance.CheckCanBuy(_price))
                    {
                        GameDataModel.Instance.AddOrReduceItem(_shopData.id,_shopData.num);
                        //更新字体颜色和信息
                        EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESSHOPITEMTEXT);
                        EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESITEMINFO);
                    }
                },
                ()=> { });
            }
        });
    }
    private Text GetBtnEquipText()
        => _uitil.GetControl<Button>("btn_Equip").transform.Find("Text").GetComponent<Text>();
    private void SetBtnActive(bool value)
    {
        _uitil.GetControl<Image>("img_btnBk").SetActive(value);
        _uitil.GetControl<Button>("btn_Equip").SetActive(value);
        _uitil.GetControl<Button>("btn_Back").SetActive(value);
    }
    private void SetArrowActive(bool value)
    {
        if (value && !transform.Find("arrows").gameObject.activeSelf)
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.Enter);
        transform.Find("arrows").SetActive(value);
    }
    private void SetEquipActive(bool value)
     => _uitil.GetControl<Text>("txt_Equip").SetActive(value);
    //显示价格信息
    private void ShowPriceInfo()
    {
        _uitil.GetControl<Image>("img_coin").SetActive(true);
        _uitil.GetControl<Text>("txt_CoinNum").SetActive(true);
        _uitil.GetControl<Text>("txt_CoinNum").SetText(_price);
        RefreshTextColor();
    }
    private void RefreshTextColor(params object[] args)
    {
        if (GameDataModel.Instance.GetCurCoin() < _price)
            _uitil.GetControl<Text>("txt_CoinNum").color = Color.red;
    }
}