/****************************************************
    文件：ItemElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/20 20:17:14
	功能：物品格子信息
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemElement : MonoBehaviour
{
    public int id;
    private UIUtil _uitil;
    public void Init(Item data, int num)
    {
        _uitil = gameObject.AddOrGet<UIUtil>();
        _uitil.Init();
        this.id = UIInfoModel.Instance.GetItemId(data);
        RefreshShow(data, num);
        InitEnterEvent();
        SetArrowActive(false);
        SetBtnActive(false);
        CheckIsEquip(data);
        InitClickEvent(data);
    }
    //检测当前物品是否被装备
    private void CheckIsEquip(Item data)
    {
        SetEquipActive(false);
        if (data.tpye == E_ItemTpye.workable && GameDataModel.Instance.CheckCurItemIsEquiped(id))
            SetEquipActive(true);
    }
    private void RefreshShow(Item data, int num)
    {
        _uitil.GetControl<Image>("img_Icon").SetImage(data.sprite);
        _uitil.GetControl<Text>("txt_Name").SetText(data.name);
        _uitil.GetControl<Text>("txt_Num").SetText(num);
        if (num <= 1)
            _uitil.GetControl<Text>("txt_Num").SetActive(false);
    }
    private void InitEnterEvent()
    {
        _uitil.GetControl<Image>("img_bk").AddOrGet<UIEventUtil>().OnEnterEvent += ((data) =>
          {
              SetArrowActive(true);
              UIInfoModel.Instance.CurItemId = id;
              EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESITEMINFO);
          });
        _uitil.GetControl<Image>("img_bk").AddOrGet<UIEventUtil>().OnExitEvent += ((data) => SetArrowActive(false));
    }
    private void InitClickEvent(Item data)
    {
        if (data.tpye != E_ItemTpye.workable)
            return;
        GetBtnEquipText().text = GameDataModel.Instance.CheckCurItemIsEquiped(id) ? "卸下" : "装备";
        _uitil.GetControl<Image>("img_bk").AddOrGet<UIEventUtil>().OnClickEvent += ((eventData) =>
        {
            SetBtnActive(true);
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.NormalClick);
        });
        _uitil.GetControl<Image>("btn_Equip").AddOrGet<UIEventUtil>().OnEnterEvent += (eventData) =>
            {

                _uitil.GetControl<Image>("btn_Equip").transform.Find("arrows").SetActive(true);
                AudioMgr.Instance.PlayUIMusic(E_UIMusic.Enter);
            };
        _uitil.GetControl<Image>("btn_Back").AddOrGet<UIEventUtil>().OnEnterEvent += (eventData) =>
           {
               _uitil.GetControl<Image>("btn_Back").transform.Find("arrows").SetActive(true);
               AudioMgr.Instance.PlayUIMusic(E_UIMusic.Enter);
           };
        _uitil.GetControl<Image>("btn_Equip").AddOrGet<UIEventUtil>().OnExitEvent += (eventData)
            => _uitil.GetControl<Image>("btn_Equip").transform.Find("arrows").SetActive(false);
        _uitil.GetControl<Image>("btn_Back").AddOrGet<UIEventUtil>().OnExitEvent += (eventData)
            => _uitil.GetControl<Image>("btn_Back").transform.Find("arrows").SetActive(false);
        _uitil.GetControl<Button>("btn_Equip").SetEvent(() =>
        {
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.NormalClick);
            var btnName = GetBtnEquipText().text;
            switch (btnName)
            {
                case "装备":
                    GameDataModel.Instance.EquipItem(id);
                    GetBtnEquipText().text = "卸下";
                    SetEquipActive(true);
                    SetBtnActive(false);
                    break;
                case "卸下":
                    GameDataModel.Instance.UnEquipItem(id);
                    GetBtnEquipText().text = "装备";
                    SetEquipActive(false);
                    SetBtnActive(false);
                    break;
            }
        });
        _uitil.GetControl<Button>("btn_Back").SetEvent(() =>
        {
            SetBtnActive(false);
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.NormalClick);
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
        if (value)
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.Enter);
        transform.Find("arrows").SetActive(value);
    }
    private void SetEquipActive(bool value)
        => _uitil.GetControl<Text>("txt_Equip").SetActive(value);
}