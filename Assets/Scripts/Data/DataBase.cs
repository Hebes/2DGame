/****************************************************
    文件：DataBase.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/29 11:37:6
	功能：游戏中的相关数据
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataBase<T>
{
    public int id;
}
//音量大小
public class AudioData
{
    public double bgVolume;
    public double soundVolume;
    public double globalVolume;//全局音量大小
}
#region 游戏的存档和的读档
public class AllGameArchive
{
    public List<GameArchive> archiveList;
}
//存档数据
public class GameArchive : DataBase<GameArchive>
{
    public HeroManSettingData heroManSettingData;
    public LevelData levelData;
    public BossData bossData;
    public LevelTreasureBoxUnlockData levelTreasureBoxData;
    public string time;//存档开始的时间
    public string lastArchivePos;//最后一次存档的地方
    public int coin;//当前的金币数量
    public List<ItemInfo> workAbleList;//可使用的道具
    public List<ItemInfo> preciousList;//宝贵的物品
    public List<ItemInfo> materialsList;//材料
    public List<int> curEquipList;//当前所装备的可使用物品
}
//角色属性数据
public class HeroManSettingData
{
    public int attack=1;//当前攻击力
    public int maxHealth=5;//最大生命值
    public double maxMagicPower=50;//最大法力值
    public double lightReduceSpeed=5f;//光源流逝速度
    public double lightAddRatio = 1f;//光源值
}
//关卡做火点解锁状态
public class LevelData
{
    public Dictionary<string,List<int>> levelUnlockList;
}
//Boss解锁状态
public class BossData
{
    public List<int> bossBeatList;
}
//物品信息
public class ItemInfo
{
    public int id;
    public int num;
}
//关卡宝箱解锁状态
public class LevelTreasureBoxUnlockData
{
    public Dictionary<string, List<int>> levelTreasureBoxUnlockList;
}
#endregion





