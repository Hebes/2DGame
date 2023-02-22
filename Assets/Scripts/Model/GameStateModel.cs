/****************************************************
    文件：GameStateModel.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/29 9:49:15
	功能：游戏状态数据
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateModel : NormalSingleTon<GameStateModel>
{
    //当前游戏状态
    public GameStateModel()
    {
        CurScene = E_SceneName.InitScene;
    }
    public int CurLevelIndex => SceneManager.GetActiveScene().buildIndex - 2;
    //当前游戏场景索引
    public E_SceneName TargetScene { get; set; }
    public E_SceneName CurScene { get; set; }
    public bool CanInteractive { get; set; }//是否可以交互
    private E_GameState _curGameState;
    public E_GameState CurGameState
    {
        get => _curGameState;
        set
        {
            _curGameState = value;
            switch (value)
            {
                case E_GameState.Lose:
                case E_GameState.Win:
                case E_GameState.Pause:
                    UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_GAMEENDPANEL);
                    break;
            }
        }
    }


} 