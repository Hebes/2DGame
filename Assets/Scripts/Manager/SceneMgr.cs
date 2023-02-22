/****************************************************
    文件：SceneMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/27 20:56:57
	功能：场景加载管理器
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMgr : NormalSingleTon<SceneMgr>
{
    private AsyncOperation _asynOp;
    public int CurAsynTaskIndex { get; private set; }//当前所异步加载的场景的任务索引
    private Dictionary<E_SceneName, Action<Action>> _sceneLoadAction = new Dictionary<E_SceneName, Action<Action>>();
    private Dictionary<E_SceneName, Action> _sceneUnloadAction = new Dictionary<E_SceneName, Action>();
    private Dictionary<E_SceneName, int> _sceneTaskNum = new Dictionary<E_SceneName, int>();
    public int CurAsynSceneTaskNum { get => _sceneTaskNum[GameStateModel.Instance.TargetScene]; }
    public SceneMgr()
    {
        SceneManager.sceneLoaded += SceneManager_SceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
        InitSceneTaskNum();
    }
    //初始化异步场景的所需完成任务的数量
    private void InitSceneTaskNum()
    {
        for (E_SceneName i = E_SceneName.InitScene; i < E_SceneName.Count; i++)
            _sceneTaskNum[i] = 1;
    }
    #region 同步加载场景
    public void LoadScene(E_SceneName sceneName, Action action)
        => SceneManager.LoadScene(sceneName.ToString());
    #endregion
    #region 异步加载场景
    public void LoadSceneAsyn(E_SceneName sceneName)
    {
        ResetAsynTaskIndex();
        CoroutineMgr.Instance.Excute(AsynLoadScene(sceneName));
    }
    private IEnumerator AsynLoadScene(E_SceneName sceneName)
    {
        _asynOp = SceneManager.LoadSceneAsync(sceneName.ToString());
        _asynOp.allowSceneActivation = false;
        yield return _asynOp;
    }
    #endregion
    //场景加载或者卸载会执行的相关事件
    private void SceneManager_SceneUnloaded(Scene scene)
        => ExcuteSceneUnloadEvent(scene);
    private void SceneManager_SceneLoaded(Scene scene, LoadSceneMode arg1)
        => ExcuteSceneLoadEvent(scene);
    private void ExcuteSceneLoadEvent(Scene scene)
    {
        E_SceneName targetSceneName = (E_SceneName)Enum.Parse(typeof(E_SceneName), scene.name);
        if (_sceneLoadAction.ContainsKey(targetSceneName))
            _sceneLoadAction[targetSceneName]?.Invoke(AddAsynIndex);
    }
    private void ExcuteSceneUnloadEvent(Scene scene)
    {
        E_SceneName targetSceneName = (E_SceneName)Enum.Parse(typeof(E_SceneName), scene.name);
        if (_sceneUnloadAction.ContainsKey(targetSceneName))
            _sceneUnloadAction[targetSceneName]?.Invoke();
    }
    public void AddSceneLoadEvent(E_SceneName targetScene, Action<Action> action)
    {
        _sceneTaskNum[targetScene] += 1;
        if (_sceneLoadAction.ContainsKey(targetScene))
            _sceneLoadAction[targetScene] += action;
        else
        {
            _sceneLoadAction[targetScene] = action;
        }
    }
    public void AddSceneUnLoadEvent(E_SceneName targetScene, Action action)
    {
        if (_sceneUnloadAction.ContainsKey(targetScene))
            _sceneUnloadAction[targetScene] += action;
        else
            _sceneUnloadAction[targetScene] = action;
    }
    public float Process()
    {
        float ratio = (float)CurAsynTaskIndex / CurAsynSceneTaskNum;
        if (_asynOp!=null&&_asynOp.progress >= 0.9f)
            FinshAsynSceneLoad();
        return ratio;
    }
    //完成异步场景加载
    private void FinshAsynSceneLoad()
    {
        AddAsynIndex();
        _asynOp.allowSceneActivation = true;
        _asynOp = null;
        //还需要LoadingPanel中做额外的逻辑判断
        //所以这里不直接置空TargetScene
        GameStateModel.Instance.CurScene = GameStateModel.Instance.TargetScene;
    }
    private void AddAsynIndex()
        => CurAsynTaskIndex++;
    //还原异步加加载场景的当前的场景索引
    private void ResetAsynTaskIndex()
        => CurAsynTaskIndex = 0;
    public bool IsScene(E_SceneName sceneName)
        => GameStateModel.Instance.CurScene == sceneName;
}