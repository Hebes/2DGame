/****************************************************
    文件：HpItemsElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/10 11:37:4
	功能：GamePanel中管理血条集合
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HpItemsElement : UIElement
{
    private float _xOffset = 250;
    private List<HpItem> _hpItemsList=new List<HpItem>();
    public override void InitChild()
    {
        EventMgr.Instance.AddEvent(E_EventName.UI_REFRESHPLAYERHP, RefreshHpItems);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventMgr.Instance.RemoveEvent(E_EventName.UI_REFRESHPLAYERHP, RefreshHpItems);
    }
    public override void Show()
    {
        base.Show();
        var playerSetting = GameDataModel.Instance.GetHeroManSettingData;
        var maxHeath = playerSetting.maxHealth;
        if (_hpItemsList.Count < maxHeath)
            CreateHpItems(maxHeath - _hpItemsList.Count);
        else if (transform.childCount > maxHeath)
            DestroySurplusItems(_hpItemsList.Count - maxHeath);
    }
    //创造血条子对象
    private void CreateHpItems(int num)
    {
        var itemRes = ResMgr.Instance.LoadPrefab(Paths.PREFAB_UIELEMENT_HPITEMELEMENT);
        float width = itemRes.transform.Rect().sizeDelta.x;
        GameObject tempGo = null;
        HpItem item = null;
        for (int i = 0; i < num; i++)
        {
            tempGo = Instantiate(itemRes, transform);
            item = tempGo.AddOrGet<HpItem>();
            item.Init();
            _hpItemsList.Add(item);
            tempGo.transform.Rect().anchoredPosition = new Vector2(_xOffset + width * (_hpItemsList.Count - 1), 0);
        }
        if (num == 1)
            tempGo.SetActive(false);
    }
    //删除多余的血条子对象
    private void DestroySurplusItems(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Destroy(_hpItemsList[_hpItemsList.Count - 1].gameObject);
            _hpItemsList.RemoveAt(_hpItemsList.Count - 1);
        }
    }
    private void RefreshHpItems(params object[] args)
    {
        int curHp = Convert.ToInt32((float)args[0]);
        for (int i = 0; i < _hpItemsList.Count; i++)
            _hpItemsList[i].SetActive(i <= curHp - 1);
    }
}