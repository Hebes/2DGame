/****************************************************
    文件：GamePlaceItem.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/12 17:46:1
	功能：GamePlaceItemsElement下面的单个地名管理集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GamePlaceItem : MonoBehaviour
{
    private int _levelIndex;
    private int _placeIndex;
    private UIUtil _uitl;
    public void Init(int levelIndex, int placeIndex)
    {
        _levelIndex = levelIndex;
        _placeIndex = placeIndex;
        _uitl = gameObject.AddOrGet<UIUtil>();
        _uitl.Init();
        InitUIFunction();
    }
    private void InitUIFunction()
    {
        _uitl.GetControl<Image>("img_Hero").SetActive(false);
        _uitl.GetControl<Image>("img_Arrow").SetActive(false);
        _uitl.GetControl<Button>("btn_SelectPlace").interactable = false;
        _uitl.GetControl<Text>("txt_PlaceName").SetText(UIInfoModel.Instance.GetGamePlaceName(_levelIndex, _placeIndex));
        var curPlace = UIInfoModel.Instance.CurPlace;
        //如果当前的位置就是在这里
        if (curPlace.Key == _levelIndex && curPlace.Value == _placeIndex)
        {
            _uitl.GetControl<Image>("img_Hero").SetActive(true);
            return;
        }
        _uitl.GetControl<Button>("btn_SelectPlace").interactable = true;
        _uitl.GetControl<Button>("btn_SelectPlace").AddOrGet<UIEventUtil>()
            .OnEnterEvent += (data) =>
            {
                AudioMgr.Instance.PlayUIMusic(E_UIMusic.Enter);
                _uitl.GetControl<Image>("img_Arrow").SetActive(true);
            };
        _uitl.GetControl<Button>("btn_SelectPlace").AddOrGet<UIEventUtil>()
            .OnExitEvent += (data) => _uitl.GetControl<Image>("img_Arrow").SetActive(false);
        _uitl.GetControl<Button>("btn_SelectPlace").SetEvent(() => 
        {
            AudioMgr.Instance.PlayUIMusic(E_UIMusic.ChooseArchive);
            //设置目标点位作为最后存档点位
            GameDataModel.Instance.SetLastArchiviePos(_levelIndex,_placeIndex);
            GameDataModel.Instance.SaveAllArchivie();
            //关闭当前面板
            UIManager.Instance.BackPanel();
            UIInfoModel.Instance.InitCurLookBackLevelId(_levelIndex);
            GameStateModel.Instance.TargetScene = GameDataModel.Instance.GetTargetSceneNameBySceneIndex(_levelIndex);
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_LOADINGPANEL);
        });
    }
}