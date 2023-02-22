/****************************************************
    文件：GameDataMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/26 17:11:45
	功能：游戏数据管理器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class GameDataMgr : NormalSingleTon<GameDataMgr>, IInit, IReader
{
    private IReader _reader;//游戏数据读取接口
    public AudioData AudioData { get; private set; }
    public AllGameArchive AllGameArchive { get; private set; }
    public GameDataMgr()
    {
        _reader = new JsonReader();
    }
    public void Init()
    {
        CreateAboutFolder();
        InitData();
    }
    private void CreateAboutFolder()
    {
        if (!Directory.Exists(Paths.GAMEDATA_FOLDER))
            Directory.CreateDirectory(Paths.GAMEDATA_FOLDER);
    }
    private void InitData()
    {
        InitAudioVolumeData();
        InitAllGameArchive();
    }
    #region 音频数据相关
    private void InitAudioVolumeData()
    {
        AudioData = LoadData<AudioData>(Consts.DATA_AUDIO);
        if (AudioData == null)
            ResetAudioVolumeData();
    }
    public void ResetAudioVolumeData()
    {
        AudioData = new AudioData
        {
            bgVolume = 1,
            soundVolume = 1
        };
    }
    public void SaveAudioVolumeData()
        => SaveData(AudioData, Consts.DATA_AUDIO);
    #endregion
    #region 存档数据相关
    private void InitAllGameArchive()
        => AllGameArchive = LoadData<AllGameArchive>(Consts.DATA_ALLGAMEARCHIVE);
    public void SaveAllGameArchive()
        => SaveData(AllGameArchive, Consts.DATA_ALLGAMEARCHIVE);
    //清除指定索引的存档
    public bool ClearSelectIndexArchive(int index)
    {
        if (AllGameArchive.archiveList.Count > index)
        {
            AllGameArchive.archiveList.RemoveAt(index);
            SaveAllGameArchive();
            return true;
        }
        else
            Debug.LogError($"传入索引错误:{index}");
        return false;
    }
    //得到指定索引的存档信息
    public GameArchive GetSelectIndexGameArchive(int index)
    {
        if (AllGameArchive != null && AllGameArchive.archiveList.Count > index)
            return AllGameArchive.archiveList[index];
        return null;
    }
    //创建默认存档
    public GameArchive CreateNewDefaultArchive()
    {
        var gameArchive = new GameArchive()
        {
            heroManSettingData = new HeroManSettingData(),
            levelData = new LevelData()
            {
                levelUnlockList = new Dictionary<string, List<int>> { { "0", new List<int>() { 0 } } }//默认解锁第一关的第一个地点
            },
            bossData = new BossData
            {
                bossBeatList = new List<int>()
            },
            time = GetNowTime(),
            lastArchivePos = "0|0",//默认最后一次存档的地方为第一关的第一个地方
            coin = 0,
            workAbleList = new List<ItemInfo>() { },
            preciousList = new List<ItemInfo> { new ItemInfo { id = 0, num = 1 } },
            materialsList = new List<ItemInfo>() { },
            curEquipList = new List<int>(),
            levelTreasureBoxData=new LevelTreasureBoxUnlockData()
            {
                levelTreasureBoxUnlockList=new Dictionary<string, List<int>>() { { "0", new List<int>() {  } } }
            }
        };
        if (AllGameArchive == null)
        {
            AllGameArchive = new AllGameArchive();
            AllGameArchive.archiveList = new List<GameArchive>();
            gameArchive.id = 0;
        }
        else
            gameArchive.id = AllGameArchive.archiveList.Count;
        AllGameArchive.archiveList.Add(gameArchive);
        SaveAllGameArchive();
        return gameArchive;
    }
    private string GetNowTime()
    {
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;
        var day = DateTime.Now.Day;
        var hour = DateTime.Now.Hour;
        var minute = DateTime.Now.Minute;
        var second = DateTime.Now.Second;
        //格式化显示当前时间
        return $"{year}/{month}/{day} {hour}:{minute}:{second}";
    }
    #endregion
    public T LoadData<T>(string fileName) where T : class, new()
        => _reader.LoadData<T>(fileName);
    public void SaveData(object data, string fileName)
        => _reader.SaveData(data, fileName);
}