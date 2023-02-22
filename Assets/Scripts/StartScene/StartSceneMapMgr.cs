/****************************************************
    文件：StartSceneMapMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/27 20:46:10
	功能：游戏开始场景地图管理器
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class StartSceneMapMgr : MonoDestroySingleTon<StartSceneMapMgr>
{
    private float _playerBackRatio = 0.5f;
    private StartScenePlayerController _playerController;
    private List<UVTransformMove> _uvList = new List<UVTransformMove>();
    private DecorationCreator _decorationCreator;
    private Dictionary<string, float> _mapUvXSpeed = new Dictionary<string, float>()
    {
        { "Background",0.001f},
        { "Background1",0.002f},
        { "Background2",0.003f},
        { "Background3",0.1f},
        { "Platform",0.5f},
        { "Foreground",0.6f},
        { "Foreground1",0.7f},
    };
    public  async void Init(Action callback)
    {
        InitPlayer();
        await Task.Delay(TimeSpan.FromSeconds(1f));
        InitDecorationCreator();
        await Task.Delay(TimeSpan.FromSeconds(1f));
        InitMapMove();
        await Task.Delay(TimeSpan.FromSeconds(1f));
        callback?.Invoke();
    }
    private void Update()
    {
        if (_playerController == null || _decorationCreator == null||_uvList.Count==0)
            return;
        _playerController.UpdatePlayerMove();
        //根据玩家移动来设置场景其他物体的相对移动速度
        if (!_playerController.IsForward)
        {
            _decorationCreator.UpdateDecorationMove(1 * _playerBackRatio);
            foreach (var uv in _uvList)
                uv.UpdateMove(1 * _playerBackRatio);
        }
        else
        {
            _decorationCreator.UpdateDecorationMove();
            foreach (var uv in _uvList)
                uv.UpdateMove();
        }
    }
    //初始化主角
    private void InitPlayer()
    {
        var playerGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_HERO_STATRSCENE);
        var screeMinX = ScreenPosUtil.GetScreenMinPos().x;
        var screenMidY = ScreenPosUtil.GetScreenCenterPos().y;
        var playerHeight = playerGo.GetComponent<SpriteRenderer>().bounds.size.y;
        var groundPos = GroundPosUtil.GetTargetGroundPos(new Vector2(screeMinX + playerHeight / 2f, screenMidY));
        playerGo.transform.position = groundPos;
        _playerController = playerGo.AddOrGet<StartScenePlayerController>();
        _playerController.Init();
    }
    //初始化装饰物体生成器
    private void InitDecorationCreator()
    {
        _decorationCreator = transform.Find("DecorationCreator").AddOrGet<DecorationCreator>();
        _decorationCreator.Init();
    }
    //初始化地图移动组件
    private void InitMapMove()
    {
        foreach (KeyValuePair<string, float> kv in _mapUvXSpeed)
        {
            var uvMove = transform.Find(kv.Key).AddOrGet<UVTransformMove>();
            uvMove.InitUVTrans(new Vector2(kv.Value, 0), 1);
            _uvList.Add(uvMove);
        }
    }
}