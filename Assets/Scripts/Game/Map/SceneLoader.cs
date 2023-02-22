/****************************************************
    文件：SceneLoder.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/29 15:17:28
	功能：游戏中的场景跳转器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneLoader : MonoBehaviour 
{
    [Header("相关设置"),SerializeField]
    private int targetLevel;//目标关卡
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.Player))
        {
            switch (targetLevel)
            {
                case 0:
                    GameStateModel.Instance.TargetScene = E_SceneName.LevelOne;
                    break;
                case 1:
                    GameStateModel.Instance.TargetScene = E_SceneName.LevelTwo;
                    break;
            }
            //如果没有解锁  关卡 则解锁当前关卡
            GameDataModel.Instance.UnlockLevel(targetLevel);
            //设置篝火面板选择关卡
            UIInfoModel.Instance.InitCurLookBackLevelId(targetLevel);
            //停止对玩家的控制
            EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_CANTCTROL,false);
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_LOADINGPANEL);
        }
    }
}