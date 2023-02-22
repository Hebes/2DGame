/****************************************************
    文件：GameLevelItemsElement.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/12 17:45:2
	功能：LookBackElement下面的关卡名管理集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameLevelItemsElement : UIElement
{
    public override void InitChild()
    {

    }
    public override void Show()
    {
        base.Show();
        CreateGameLevelItems();
    }
    //刷新游戏关卡显示子物体
    private void CreateGameLevelItems()
    {
        var levels = GameDataModel.Instance.GetUnlockLevels();
        bool isCreate = false;
        while(transform.childCount < levels.Count)
        {
            isCreate = true;
            var levelGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIELEMENT_GAMELEVEITEM,transform);
        }
        for (int i = 0; i < levels.Count; i++)
            transform.GetChild(i).AddOrGet<GameLevelItem>().Init(i);
        if (isCreate)
            Reacquire();
    }
}