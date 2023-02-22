/****************************************************
    文件：GameModel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/26 20:57:22
	功能：游戏时的数据管理
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameDataModel : NormalSingleTon<GameDataModel>
{
    //当前的游戏存档数据
    public GameArchive CurGameArchive { get; private set; }
    //当前的游戏存档对应的玩家属性数据
    public HeroManSettingData GetHeroManSettingData => CurGameArchive.heroManSettingData;
    //当前的游戏存档对应关卡数据
    public LevelData LevelData => CurGameArchive.levelData;
    public BossData BossData => CurGameArchive.bossData;
    public LevelTreasureBoxUnlockData LevelTreasureBoxData => CurGameArchive.levelTreasureBoxData;
    //设置当前选择的游戏存档
    public void SetCurGameArchive(GameArchive gameArchive)
        => CurGameArchive = gameArchive;
    public bool CheckCanBuy(int value)
    {
        int curCoin = CurGameArchive.coin;
        int result = curCoin -= value;
        if (result >= 0)
        {
            AudioMgr.Instance.PlayOnce("hero_shop");
            CurGameArchive.coin = result;
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESCOIN, CurGameArchive.coin,-value);
            SaveAllArchivie();
            return true;
        }
        AudioMgr.Instance.PlayOnce("error");
        return false;
    }
    //增加或者减少金币
    public void AddOrReduceCoin(int coin)
    {
        if (coin > 0)
            AudioMgr.Instance.PlayOnce("hero_getCoin");
        else
            AudioMgr.Instance.PlayOnce("hero_shop");
        int curCoin = CurGameArchive.coin + coin;
        if (curCoin <= 0)
            curCoin = 0;
        CurGameArchive.coin = curCoin;
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESCOIN, CurGameArchive.coin,coin);
        SaveAllArchivie();
    }
    //得到当前金币数量
    public int GetCurCoin()
        => CurGameArchive.coin;
    //解锁存档点
    public void UnlockScenePlace(int sceneIndex, int placeId)
    {
        if (LevelData.levelUnlockList.TryGetValue(sceneIndex.ToString(), out List<int> unlockPlaceList))
        {
            if (!unlockPlaceList.Contains(placeId))
            {
                unlockPlaceList.Add(placeId);
                GameStateModel.Instance.CurGameState = E_GameState.Pause;
            }
            SetLastArchiviePos(sceneIndex, placeId);
            SaveAllArchivie();
        }
        else
            Debug.LogError($"场景索引:{sceneIndex}不存在");
    }
    //设置最后一个存档点
    public void SetLastArchiviePos(int sceneIndex, int placeId)
        => CurGameArchive.lastArchivePos = $"{sceneIndex}|{placeId}";
    //解锁游戏关卡
    public void UnlockLevel(int levelIndex)
    {
        if (!LevelData.levelUnlockList.ContainsKey(levelIndex.ToString()))
        {
            LevelData.levelUnlockList[levelIndex.ToString()] = new List<int>() { 0 };
            UnlockScenePlace(levelIndex,0);
            LevelTreasureBoxData.levelTreasureBoxUnlockList[levelIndex.ToString()] = new List<int>();
            SaveAllArchivie();
        }
    }
    //保存所有存档
    public void SaveAllArchivie()
        => GameDataMgr.Instance.SaveAllGameArchive();
    public int GetLastArchivePosId()
    {
        var id = int.Parse(CurGameArchive.lastArchivePos.Split('|')[1]);
        return id;
    }
    //得到所有已经解锁的关卡
    public List<int> GetUnlockLevels()
    {
        List<int> temp = new List<int>();
        foreach (var data in LevelData.levelUnlockList.Keys)
        {
            var index = int.Parse(data);
            if (!temp.Contains(index))
                temp.Add(index);
        }
        return temp;
    }
    public List<int> GetLevelUnlockPlace(int sceneIndex)
    {
        if (LevelData.levelUnlockList.TryGetValue(sceneIndex.ToString(), out List<int> temp))
            return temp;
        else
            Debug.LogError($"游戏Level索引:{sceneIndex}不存在");
        return temp;
    }
    //得到目标场景名 通过场景索引
    public E_SceneName GetTargetSceneNameBySceneIndex(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                return E_SceneName.LevelOne;
            case 1:
                return E_SceneName.LevelTwo;
        }
        Debug.LogError($"当前场景索引:{sceneIndex}不存在对应的关卡场景名");
        return E_SceneName.LevelOne;
    }
    //解锁Boss
    public void UnlockBoss(int id)
    {
        if (!BossData.bossBeatList.Contains(id))
        {
            BossData.bossBeatList.Add(id);
            SaveAllArchivie();
        }
    }
    //检测玩家是否基本击败当前Boss
    public bool CheckIsBeatCurBossById(int id)
        => BossData.bossBeatList.Contains(id);
    //得到指定物品的数量
    public int GetItemNum(int index)
    {
        foreach (var item in CurGameArchive.workAbleList)
        {
            if (item.id == index)
                return item.num;
        }
        foreach (var item in CurGameArchive.preciousList)
        {
            if (item.id == index)
                return item.num;
        }
        foreach (var item in CurGameArchive.materialsList)
        {
            if (item.id == index)
                return item.num;
        }
        return 0;
    }
    public ItemInfo GetPlayerItemInfoById(int id)
    {
        foreach (var item in CurGameArchive.workAbleList)
        {
            if (item.id == id)
                return item;
        }
        foreach (var item in CurGameArchive.preciousList)
        {
            if (item.id == id)
                return item;
        }
        foreach (var item in CurGameArchive.materialsList)
        {
            if (item.id == id)
                return item;
        }
        return null;
    }
    //得到当前主角所拥有的物体
    public List<ItemInfo> GetPlayerContainItemInfos(int index)
    {
        var type = (E_ItemTpye)index;
        switch (type)
        {
            case E_ItemTpye.workable:
                return CurGameArchive.workAbleList;
            case E_ItemTpye.precious:
                return CurGameArchive.preciousList;
            case E_ItemTpye.materials:
                return CurGameArchive.materialsList;
        }
        return null;
    }
    //得到当前主角所装备的药品
    public List<int> GetPlayerEquipItemInfos()
        => CurGameArchive.curEquipList;
    //检测当前物体是否被装备
    public bool CheckCurItemIsEquiped(int id)
    {
        if (GetPlayerEquipItemInfos().Contains(id))
            return true;
        return false;
    }
    //装备指定物品
    public void EquipItem(int id)
    {
        if (!GetPlayerEquipItemInfos().Contains(id))
        {
            GetPlayerEquipItemInfos().Add(id);
            SaveAllArchivie();
        }
        else
            Debug.LogError($"id为{id}已被装备");
    }
    //卸下指定装备
    public void UnEquipItem(int id)
    {
        if (GetPlayerEquipItemInfos().Contains(id))
        {
            GetPlayerEquipItemInfos().Remove(id);
            SaveAllArchivie();
        }
        else
            Debug.LogError($"id为{id}未被装备");
    }
    //增减或者减少物品
    public void AddOrReduceItem(int id, int addOrReduceNum)
    {
        var itemInfo = GetPlayerItemInfoById(id);
        if (addOrReduceNum > 0 && itemInfo == null)
        {
            itemInfo = new ItemInfo() { id = id, num = 1 };
            var itemData = UIInfoModel.Instance.GetItemData(id);
            var list = GetPlayerContainItemInfos((int)itemData.tpye);
            list.Add(itemInfo);
            //按照id 从小到大排序
            list.Sort((a, b) => a.id.CompareTo(b.id));
        }
        else
            itemInfo.num += addOrReduceNum;
        if (itemInfo.num == 0)
        {
            //得到该对应的列表 并删除该物品
            var itemData = UIInfoModel.Instance.GetItemData(id);
            List<ItemInfo> list = null;
            switch (itemData.tpye)
            {
                case E_ItemTpye.workable:
                    list = GetPlayerContainItemInfos((int)E_ItemTpye.workable);
                    GetPlayerEquipItemInfos().Remove(itemInfo.id);
                    break;
                case E_ItemTpye.precious:
                    list = GetPlayerContainItemInfos((int)E_ItemTpye.precious);
                    break;
                case E_ItemTpye.materials:
                    list = GetPlayerContainItemInfos((int)E_ItemTpye.materials);
                    break;
            }
            list.Remove(itemInfo);
        }
        if (itemInfo.num < 0)
        {
            Debug.LogError($"id为:{id}的物品数量为负数");
            return;
        }
        SaveAllArchivie();
    }
    //检测是否可以淬炼武器
    public bool CheckCanQueching()
    {
        var list = GetPlayerContainItemInfos((int)E_ItemTpye.materials);
        foreach (var itemInfo in list)
        {
            if (itemInfo.id == 3 && itemInfo.num >= 4)
            {
                GetHeroManSettingData.attack += 1;
                AddOrReduceItem(itemInfo.id, -4);
                EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_ADDATTACK);
                return true;
            }
        }
        return false;
    }
    //检测是否可以提升体力
    public bool CheckCanStrength()
    {
        var list = GetPlayerContainItemInfos((int)E_ItemTpye.materials);
        foreach (var itemInfo in list)
        {
            if (itemInfo.id == 4 && itemInfo.num >= 4)
            {
                GetHeroManSettingData.maxHealth += 1;
                GetHeroManSettingData.maxMagicPower += 10;
                AddOrReduceItem(itemInfo.id, -4);
                EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_ADDMAXHEALTH);
                EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_ADDMAXMAGICPOWER);
                return true;
            }
        }
        return false;
    }
    //得到宝箱的解锁状态
    public bool GetTreasureboxUnlockState(int levelIndex, int id)
    {
        if (LevelTreasureBoxData.levelTreasureBoxUnlockList.TryGetValue(levelIndex.ToString(), out List<int> list))
        {
            if (list.Contains(id))
                return true;
        }
        else
            Debug.LogError($"关卡索引为:{levelIndex}的关卡未解锁");
        return false;
    }
    //解锁指定关卡的宝箱
    public void UnlockLevelTreasurebox(int levelIndex, int id)
    {
        if (LevelTreasureBoxData.levelTreasureBoxUnlockList.TryGetValue(levelIndex.ToString(), out List<int> list))
        {
            if (!list.Contains(id))
            {
                list.Add(id);
                SaveAllArchivie();
            }
        }
        else
            Debug.LogError($"关卡索引为:{levelIndex}的关卡未解锁");
    }


}