/****************************************************
    文件：UIManager.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 11:13:46
	功能：UI管理者
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : NormalSingleTon<UIManager>
{
    private Dictionary<string, PanelBase> _panelDic = new Dictionary<string, PanelBase>();
    private Stack<string> _panelStack = new Stack<string>();
    private Transform _canvasTf;
    public Transform CanvasTf
    {
        get
        {
            if (_canvasTf == null)
            {
                _canvasTf = GameObject.FindObjectOfType<Canvas>().transform;
                if (_canvasTf == null)
                    Debug.LogError($"当前场景中不存在Canvas");
            }
            return _canvasTf;
        }
    }
    //不进入UI栈的Panel路径
    private HashSet<string> _skipPopStackPanel = new HashSet<string>()
    {
        Paths.PREFAB_UIPANEL_LOADINGPANEL,
        Paths.PREFAB_UIPANEL_CHOOSEGAMEARCHIVEPANEL,
        Paths.PREFAB_UIPANEL_GAMEENDPANEL,
        Paths.PREFAB_UIPANEL_GAMEBLACKPANEL,
    };
    public PanelBase ShowPanel(string path)
    {
        if (_panelStack.Count>0)
        {
            string lastPanelPath = _panelStack.Peek();
            var lastPanel = _panelDic[lastPanelPath];
            if (GetPanelLayer(path) <= GetPanelLayer(lastPanelPath))
                lastPanel.Hide();
        }
        //忽略的就不会加入栈中
        if (!_skipPopStackPanel.Contains(path))
            _panelStack.Push(path);
        var panel = GetPanel(path);
        panel.Show();
        return panel;
    }
    //强迫隐藏Panel
    public PanelBase HidePanel(string path)
    {
        var panel = _panelDic[path];
        panel.Hide();
        return panel;
    }
    public void BackPanel()
    {
        //至少需要一个Panel
        if (_panelStack.Count <= 1)
            return;
        string lastPanelPath = _panelStack.Pop();
        _panelDic[lastPanelPath].Hide();
        string path = _panelStack.Peek();
        _panelDic[path].Show();
    }
    public PanelBase GetPanel(string path)
    {
        PanelBase panel=null;
        if(!_panelDic.TryGetValue(path,out panel))
        {
            GameObject panelGo = ResMgr.Instance.LoadPrefabAndInstantiate(path, CanvasTf);
            //todo初始化层级
            panel = panelGo.GetComponent<PanelBase>();
            if(panel==null)
            {
                Debug.LogError($"路径为：{path}的Panel身上未挂载PanelBase脚本");
                return null;
            }
            panel.Init();
            InitLayer(path,panel);
            _panelDic[path] = panel;
        }
        return panel;
    }
    private void InitLayer(string path, PanelBase panel)
        => UILayerMgr.Instance.SetLayer(path, panel.transform);
    private E_UILayer GetPanelLayer(string path)
        => UILayerMgr.Instance.GetUILayer(path);
    //检测某面板是否存在
    public bool CheckExistPanel(string path)
    {
        if (_panelDic.ContainsKey(path))
            return true;
        return false;
    }
    //得到面板的显隐状态
    public bool GetPanelActive(string path)
    {
        if (_panelDic.ContainsKey(path))
            return _panelDic[path].GetActive();
            return false;
    }
}