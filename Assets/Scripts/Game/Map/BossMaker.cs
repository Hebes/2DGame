/****************************************************
    文件：BossMaker.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/18 18:28:21
	功能：Boss生成器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Threading.Tasks;

public class BossMaker : MonoBehaviour
{
    [SerializeField, BoxGroup("Info", ShowLabel = false), LabelText("Boss编号")]
    private int id;
    [SerializeField, BoxGroup("Info", ShowLabel = false), LabelText("Boss预制体"), PreviewField(75)]
    private GameObject bossPrefab;
    [SerializeField, BoxGroup("Info", ShowLabel = false), LabelText("Boss延迟生成时间")]
    private float delayCreateTime;
    private Door[] _doors;
    private bool isClose = false;
    private string _closeDoorClipName = "door_close";
    private string _openDoorClipName = "door_open";
    private void Awake()
    {
        EventMgr.Instance.AddEvent(E_EventName.BOSS_DEAD,OpenDoor);
        if (GameDataModel.Instance.CheckIsBeatCurBossById(id))
        {
            Destroy(gameObject);
            return;
        }
        InitDoor();
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(E_EventName.BOSS_DEAD, OpenDoor);
    }
    private void InitDoor()
    {
        _doors = new Door[2];
        var doorOne = transform.Find("DoorOne").AddOrGet<Door>();
        _doors[0] = doorOne;
        var doorTwo = transform.Find("DoorTwo").AddOrGet<Door>();
        _doors[1] = doorTwo;
        foreach (var door in _doors)
            door.Init();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player) ||isClose)
            return;
        isClose = true;
        CloseOrOpenDoors(false);
        CreateBoss();
    }
    private void CloseOrOpenDoors(bool open)
    {
        foreach (var door in _doors)
            door.CloseOrOpenDoorDoor(open);
        if (open)
            AudioMgr.Instance.PlayOnce(_openDoorClipName);
        else
            AudioMgr.Instance.PlayOnce(_closeDoorClipName);
    }
    private async void CreateBoss()
    {
        await Task.Delay(TimeSpan.FromSeconds(delayCreateTime));
        var pos = transform.Find("BossMakerPos").position;
        var bossGo = Instantiate(bossPrefab);
        bossGo.transform.position = pos;
        PlayBattleMusic();
    }
    private void OpenDoor(params object[] args)
    {
        int id = (int)args[0];
        if(id==this.id)
        {
            CloseOrOpenDoors(true);
            GameDataModel.Instance.UnlockBoss(this.id);
        }
    }
    //播放战斗音乐
    private void PlayBattleMusic()
    {
        switch (id)
        {
            case 0:
                AudioMgr.Instance.PlayBattleMusic(E_BattleMusic.battlemusic_levelOne);
                break;
            case 1:
                AudioMgr.Instance.PlayBattleMusic(E_BattleMusic.battlemusic_levelTwo);
                break;
        }
    }
}