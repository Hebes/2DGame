/****************************************************
    文件：UILayerMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 11:13:33
	功能：UI层级管理器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UILayerMgr : NormalSingleTon<UILayerMgr>, IInit
{
    private static readonly Dictionary<string, E_UILayer> _uiLayerCfgDic = new Dictionary<string, E_UILayer>()
    {
        {Paths.PREFAB_UIPANEL_STARTPANEL,E_UILayer.BASE},
        {Paths.PREFAB_UIPANEL_ABOUTPANEL,E_UILayer.BASE},
        {Paths.PREFAB_UIPANEL_GAMEPANEL,E_UILayer.BASE},
        {Paths.PREFAB_UIPANEL_ARCHIVEPOINTPANEL,E_UILayer.BASE},
        {Paths.PREFAB_UIPANEL_CHATPANEL,E_UILayer.BASE},
        {Paths.PREFAB_UIPANEL_CHOOSEGAMEARCHIVEPANEL,E_UILayer.BASE},
        {Paths.PREFAB_UIPANEL_LOADINGPANEL,E_UILayer.BASE},


        {Paths.PREFAB_UIPANEL_GAMESETTINGPANEL,E_UILayer.MID},


        {Paths.PREFAB_UIPANEL_GAMEENDPANEL,E_UILayer.TOP},
        {Paths.PREFAB_UIPANEL_SETTINGPANEL,E_UILayer.TOP},
        {Paths.PREFAB_UIPANEL_BAGPANEL,E_UILayer.TOP},
        {Paths.PREFAB_UIPANEL_SHOPPANEL,E_UILayer.TOP},
        {Paths.PREFAB_UIPANEL_GAMEBLACKPANEL,E_UILayer.TOP},

        {Paths.PREFAB_UIPANEL_DIALOGPANEL,E_UILayer.DIALOG},
        {Paths.PREFAB_UIPANEL_GETITEMDIALOGPANEL,E_UILayer.DIALOG},
        {Paths.PREFAB_UIPANEL_GAMELEVELTIPDIALOGPANEL,E_UILayer.DIALOG},
    };
    public Dictionary<E_UILayer, RectTransform> _uiLayerDic;
    public void Init()
    {
        _uiLayerDic = new Dictionary<E_UILayer, RectTransform>();
        var canvasTf = GameObject.FindObjectOfType<Canvas>().transform;
        foreach (E_UILayer layer in Enum.GetValues(typeof(E_UILayer)))
        {
            var layerGo = new GameObject(layer.ToString(),typeof(RectTransform));
            var rect = layerGo.GetComponent<RectTransform>();
            rect.SetParent(canvasTf);
            rect.anchoredPosition = Vector3.zero;
            _uiLayerDic[layer] = rect;
        }
    }
    public void SetLayer(string path, Transform trans)
    {
        var parent = _uiLayerDic[GetUILayer(path)];
        trans.SetParent(parent,true);
    }
    public E_UILayer GetUILayer(string path)
    {
        if (_uiLayerCfgDic.TryGetValue(path, out E_UILayer layer))
            return layer;
        Debug.LogError($"当前路径:{path}对应的UILayer不存在");
        return E_UILayer.BASE;
    }
}