/****************************************************
    文件：GameRoot.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 10:59:15
	功能：游戏启动根节点
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class GameRoot : MonoSingleTon<GameRoot>
{
    protected async override void Awake()
    {
        base.Awake();
        await InitRsv();
        EnterStartScene();
    }
    //初始化服务模块
    private async Task<string> InitRsv()
    {
        HashSet<IInit> _initHs = new HashSet<IInit>()
        {
            GameDataMgr.Instance,
            InputMgr.Instance,
            UILayerMgr.Instance,
            AudioMgr.Instance,
            new SceneConfig(),
            UIInfoModel.Instance,
        };
        foreach (var init in _initHs)
        {
            init.Init();
            await Task.Delay(TimeSpan.FromSeconds(1.5f));
        }
        _initHs.Clear();
        return "InitRsvOver";
    }
    private void EnterStartScene()
    {
        GameStateModel.Instance.TargetScene = E_SceneName.StartScene;
        UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_LOADINGPANEL);
    }
}