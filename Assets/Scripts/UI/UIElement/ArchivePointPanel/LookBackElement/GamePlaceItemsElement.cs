/****************************************************
    文件：GamePlaceItemsElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/12 17:45:18
	功能：LookBackElement下面的地名管理集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GamePlaceItemsElement : UIElement
{
    private static Vector2 _offset = new Vector2(480, 300);
    private List<GameObject> _placeItems = new List<GameObject>();
    private int _curLevelIndex;
    public override void InitChild()
    {

    }
    public override void Show()
    {
        base.Show();
        _curLevelIndex = -1;
    }
    public override void Refresh()
    {
        base.Refresh();
        RefreshPlaceItems();
    }
    private void RefreshPlaceItems()
    {
        if (_curLevelIndex == UIInfoModel.Instance.CurLookBackLevelId)
            return;
        _curLevelIndex = UIInfoModel.Instance.CurLookBackLevelId;
        //刷新之前清除之前剩余的
        foreach (var placeItem in _placeItems)
            GameObject.Destroy(placeItem);
        _placeItems.Clear();
        var placesList = GameDataModel.Instance.GetLevelUnlockPlace(UIInfoModel.Instance.CurLookBackLevelId);
        _curLevelIndex = UIInfoModel.Instance.CurLookBackLevelId;
        int id = 0;
        foreach (var placeId in placesList)
        {
            var placeGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIELEMENT_GAMEPLACEITEM,transform);
            var rect = placeGo.transform.Rect();
            var height = rect.rect.height;
            rect.anchoredPosition = _offset + new Vector2(0,-height * id);
            var gamePlaceItem = placeGo.AddOrGet<GamePlaceItem>();
            gamePlaceItem.Init(_curLevelIndex,placeId);
            _placeItems.Add(placeGo);
            id++;
        }
    }
}