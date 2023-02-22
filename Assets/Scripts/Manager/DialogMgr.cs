/****************************************************
    文件：DialogMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/18 15:42:57
	功能：对话管理器
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogMgr
{
    private static Transform _canvasTf;
    public static Transform CanvasTf
    {
        get
        {
            if (_canvasTf == null)
            {
                _canvasTf = GameObject.Find("GameRoot/Canvas").GetComponent<Canvas>().transform;
                if (_canvasTf == null)
                    Debug.LogError($"当前场景中不存在Canvas");
            }
            return _canvasTf;
        }
    }
    #region 普通对话框
    public static DialogPanel ShowDialog(string title, string content, Action trueAction = null, Action falseAction = null)
    {
        var dialog = CreateDialogPanel();
        if (dialog == null)
        {
            Debug.LogError($"当前对话框身上没有对话框组件");
            return null;
        }
        dialog.InitDialog(title, content, trueAction, falseAction);
        dialog.Show();
        return dialog;
    }
    public static DialogPanel ShowDialog(string content, Action trueAction = null, Action falseAction = null)
    {
        var dialog = CreateDialogPanel();
        if (dialog == null)
        {
            Debug.LogError($"当前对话框身上没有对话框组件");
            return null;
        }
        dialog.InitDialog(content, trueAction, falseAction);
        dialog.Show();
        return dialog;
    }
    public static DialogPanel ShowDialog(string title, string content, string sureButtonText, string notSureButtonText, Action trueAction = null, Action falseAction = null)
    {
        var dialog = CreateDialogPanel();
        if (dialog == null)
        {
            Debug.LogError($"当前对话框身上没有对话框组件");
            return null;
        }
        dialog.InitDialog(title, content, sureButtonText, notSureButtonText, trueAction, falseAction);
        dialog.Show();
        return dialog;
    }
    public static DialogPanel ShowDialog(string content, string sureButtonText, string notSureButtonText, Action trueAction = null, Action falseAction = null)
    {
        var dialog = CreateDialogPanel();
        if (dialog == null)
        {
            Debug.LogError($"当前对话框身上没有对话框组件");
            return null;
        }
        dialog.InitDialog(content, sureButtonText, notSureButtonText, trueAction, falseAction);
        dialog.Show();
        return dialog;
    }
    private static DialogPanel CreateDialogPanel()
    {
        var dialogGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIPANEL_DIALOGPANEL, CanvasTf);
        var dialog = dialogGo.GetComponent<DialogPanel>();
        dialog.Init();
        InitLayer(Paths.PREFAB_UIPANEL_DIALOGPANEL, dialog);
        return dialog;
    }
    #endregion

    #region   道具对话框
    private static GetItemDialogPanel CreateGetItemDialogPanel()
    {
        var dialogGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIPANEL_GETITEMDIALOGPANEL, CanvasTf);
        var dialog = dialogGo.GetComponent<GetItemDialogPanel>();
        dialog.Init();
        InitLayer(Paths.PREFAB_UIPANEL_GETITEMDIALOGPANEL, dialog);
        return dialog;
    }
    public static GetItemDialogPanel ShowGetItemDialogPanel(int itemId,int num,Action callback=null)
    {
        var dialog = CreateGetItemDialogPanel();
        if (dialog == null)
        {
            Debug.LogError($"当前物品信息对话框身上没有对话框组件");
            return null;
        }
        dialog.InitGetItemDialog(itemId,num,callback);
        dialog.Show();
        return dialog;
    }
    #endregion
    #region     关卡提示框
    private static GameLevelTipDialogPanel CreateGameLevelTipDialogPanel()
    {
        var dialogGo = ResMgr.Instance.LoadPrefabAndInstantiate(Paths.PREFAB_UIPANEL_GAMELEVELTIPDIALOGPANEL, CanvasTf);
        var dialog = dialogGo.GetComponent<GameLevelTipDialogPanel>();
        dialog.Init();
        InitLayer(Paths.PREFAB_UIPANEL_GAMELEVELTIPDIALOGPANEL, dialog);
        return dialog;
    }
    public static GameLevelTipDialogPanel ShowGameLevelTipDialogPanel(string gameLevelName)
    {
        var dialog = CreateGameLevelTipDialogPanel();
        if (dialog == null)
        {
            Debug.LogError($"当前游戏关卡对话框身上没有对话框组件");
            return null;
        }
        dialog.InitLevelTip(gameLevelName);
        dialog.Show();
        return dialog;
    }
    #endregion

    private static void InitLayer(string path, PanelBase panel)
       => UILayerMgr.Instance.SetLayer(path, panel.transform);
}