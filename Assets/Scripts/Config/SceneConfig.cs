/****************************************************
    文件：SceneConfig.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/29 10:22:3
	功能：场景加载配置文件
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneConfig : IInit
{
    public void Init()
    {
        SceneMgr.Instance.AddSceneLoadEvent(E_SceneName.StartScene,(callback)=>
            GameObject
            .FindGameObjectWithTag(Tags.MapMgr)
            .AddOrGet<StartSceneMapMgr>().Init(callback));


        #region     LevelOne
        SceneMgr.Instance.AddSceneLoadEvent(E_SceneName.LevelOne, InitMap);
        SceneMgr.Instance.AddSceneLoadEvent(E_SceneName.LevelOne, InitPlayer);
        SceneMgr.Instance.AddSceneLoadEvent(E_SceneName.LevelOne, InitCamera);
        SceneMgr.Instance.AddSceneUnLoadEvent(E_SceneName.LevelOne, RemoveCamera);
        #endregion
        #region     LevelTwo
        SceneMgr.Instance.AddSceneLoadEvent(E_SceneName.LevelTwo, InitMap);
        SceneMgr.Instance.AddSceneLoadEvent(E_SceneName.LevelTwo, InitPlayer);
        SceneMgr.Instance.AddSceneLoadEvent(E_SceneName.LevelTwo, InitCamera);
        SceneMgr.Instance.AddSceneUnLoadEvent(E_SceneName.LevelTwo, RemoveCamera);
        #endregion
    }
    //初始化视差组件 和 主角重生组件
    private void InitMap(Action callback)
    {
        var mapMgr = GameObject.FindGameObjectWithTag(Tags.MapMgr);
        mapMgr.AddOrGet<BackgroundParallx>().Init();
        mapMgr.AddOrGet<GamePosResetUtil>().Init(callback);
    }
    //初始化主角
    private void InitPlayer(Action callback)
    {
        var playerGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_HERO_GAMESCENE);
        var id = GameDataModel.Instance.GetLastArchivePosId();
        playerGo.transform.position = GamePosResetUtil.Instance.GetArchivePos(id);
        callback?.Invoke();
    }
    //初始化主摄像机
    private void InitCamera(Action callback)
    {
        var camearaMain = Camera.main;
        camearaMain.gameObject.AddOrGet<CameraMgr>().Init(callback);
    }
    private void RemoveCamera()
    {
        var camearaMain = Camera.main;
        var cameraMgr = camearaMain.GetComponent<CameraMgr>();
        if (cameraMgr != null)//这里必须要立即移除  可能会出现无法正确添加的情况
            GameObject.DestroyImmediate(cameraMgr);
    }
}