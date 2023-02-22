/****************************************************
    文件：GamePanel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/10 11:32:52
	功能：游戏UI界面
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GamePanel : PanelBase
{
    private int _curItemIndex = 0;
    private float _showCoinMsgAnimTime = 2f;
    private float _imgAnimTime=0.8f;
    public override void InitChild()
    {
        transform.Find("HpItemsElement").Add<HpItemsElement>();
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESHPLAYERMAGIC, RefreshMagicPowerValue);
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESHPLAYERLIGHT, RefreshLightPowerValue);
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESCOIN, RefreshCoinValue);
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESGAMEPANELCURITEMINFO, RefreshCurItemValue);
        GetControl<Text>("txt_CoinMsg").SetActive(false);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESHPLAYERMAGIC, RefreshMagicPowerValue);
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESHPLAYERLIGHT, RefreshLightPowerValue);
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESCOIN, RefreshCoinValue);
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESGAMEPANELCURITEMINFO, RefreshCurItemValue);
    }
    public override void Show()
    {
        base.Show();
        ChangeCurEquipItemInfo(false);
        SetCoinNum(GameDataModel.Instance.GetCurCoin());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public override void Hide()
    {
        base.Hide();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) 
            && UIManager.Instance.GetPanelActive(Paths.PREFAB_UIPANEL_GAMEPANEL)
            && !UIManager.Instance.GetPanelActive(Paths.PREFAB_UIPANEL_GAMESETTINGPANEL)
            &&GameStateModel.Instance.CurGameState==E_GameState.NONE)
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_GAMESETTINGPANEL);
        if (!GameStateModel.Instance.CanInteractive)
            return;
        //必须主角处于可交互状态的时候 才可以切换和使用道具
        if (InputMgr.Instance.GetKeyDown(Consts.CHANGEITEM))
            ChangeCurEquipItemInfo();
        if (InputMgr.Instance.GetKeyDown(Consts.USEITEM))
            UseItem();
    }
    //切换当前物品信息
    private void ChangeCurEquipItemInfo(bool isAdd = true)
    {
        var ids = GameDataModel.Instance.GetPlayerEquipItemInfos();
        if (ids.Count <= 0)
        {
            UpdateItem(-1);
            return;
        }
        if (isAdd)
            _curItemIndex++;
        if (_curItemIndex >= ids.Count)
            _curItemIndex = 0;
        UpdateItem(ids[_curItemIndex]);
    }
    private void RefreshMagicPowerValue(params object[] args)
    {
        float value = (float)args[0];
        GetControl<Image>("img_EffectMagicPower").DOFillAmount(value,_imgAnimTime);
        GetControl<Image>("img_MagicPower").fillAmount = value;
    }
    private void RefreshLightPowerValue(params object[] args)
    {
        float value = (float)args[0];
        GetControl<Image>("img_EffectLightPower").DOFillAmount(value, _imgAnimTime);
        GetControl<Image>("img_LightPower").fillAmount = value;
    }
    private void RefreshCoinValue(params object[] args)
    {
        int coin = (int)args[0];
        int addOrReduceValue = (int)args[1];
        string msg = "";
        var text = GetControl<Text>("txt_CoinMsg");
        //如果此时有提示信息的话
        if (text.gameObject.activeSelf)
        {
            string txtMsg = text.text;
            int value = 0;
            if (txtMsg.Contains("+"))
                value = int.Parse(txtMsg.Replace("+", ""));
            else
                value = -int.Parse(txtMsg.Replace("-", ""));
            value = addOrReduceValue + value;
            msg = (value > 0 ? "+" : "") + value;
        }
        else
            msg = (addOrReduceValue > 0 ? "+" : "") + addOrReduceValue;
        SetCoinMsg(msg, () => SetCoinNum(coin));
    }
    //刷新当前物品数量
    private void RefreshCurItemValue(params object[] args)
    {
        var ids = GameDataModel.Instance.GetPlayerEquipItemInfos();
        if (ids.Count <= 0)
        {
            UpdateItem(-1);
            return;
        }
        UpdateItem(ids[_curItemIndex]);
    }
    //设置金币提示信息
    private void SetCoinMsg(string msg, Action action = null)
    {
        var text = GetControl<Text>("txt_CoinMsg");
        text.SetActive(true);
        text.SetText(msg);
        var color = text.color;
        color.a = 1f;
        text.color = color;
        text.DOKill();
        text.DOFade(0, _showCoinMsgAnimTime)
            .OnComplete(() =>
            {
                text.SetActive(false);
                text.SetText("");
                action?.Invoke();
            })
            .SetUpdate(false);
    }
    private void SetCoinNum(int coin)
        => GetControl<Text>("txt_CoinNum").SetText(coin);
    private void UpdateItem(int id)
    {
        if (id == -1)
        {
            GetControl<Image>("img_Item").SetActive(false);
            GetControl<Text>("txt_Num").SetActive(false);
            return;
        }
        GetControl<Image>("img_Item").SetActive(true);
        GetControl<Text>("txt_Num").SetActive(true);
        var info = GameDataModel.Instance.GetPlayerItemInfoById(id);
        var itemData = UIInfoModel.Instance.GetItemData(info.id);
        GetControl<Image>("img_Item").SetImage(itemData.sprite);
        GetControl<Text>("txt_Num").SetText(info.num);
        if (info.num <= 1)
            GetControl<Text>("txt_Num").SetActive(false);
    }
    //使用物品
    private void UseItem()
    {
        //没有物品的时候不能使用物品
        var ids = GameDataModel.Instance.GetPlayerEquipItemInfos();
        if (ids.Count <= 0)
            return;

        int id = ids[_curItemIndex];
        UseItemById(id);
        GameDataModel.Instance.AddOrReduceItem(id, -1);
        ChangeCurEquipItemInfo(false);
    }
    //真正使用道具产生效果
    private void UseItemById(int id)
    {
        switch (id)
        {
            case 1:
                EventMgr.Instance.ExecuteEvent(E_EventName.USEITEM_RECOOVERALLHP);
                AudioMgr.Instance.PlayOnce("hero_recover");
                break;
            case 2:
                EventMgr.Instance.ExecuteEvent(E_EventName.USEITEM_IMPROVERECOVER_TEMPORARY, true);
                AudioMgr.Instance.PlayOnce("hero_improve");
                break;
        }
    }

}