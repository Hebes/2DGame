/****************************************************
    文件：UIControllerUtil.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/26 19:26:35
	功能: UI组件获取Util
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUtil : MonoBehaviour ,IInit
{
    //符合规则的UI名配置
    private static HashSet<string> _fitUIControllerName = new HashSet<string>()
    {
        "btn_",
        "img_",
        "txt_",
        "sld_",
        "tgl_",
        "ipt_",
        "sv_"
    };
    //符合需求的UI组件配置
    private static HashSet<Type> _fitUIController = new HashSet<Type>()
    {
        typeof(Button),
        typeof(Image),
        typeof(Text),
        typeof(Slider),
        typeof(Toggle),
        typeof(InputField),
        typeof(ScrollRect),
    };
    private Dictionary<string, List<UIBehaviour>> _uiControllerDic;
    public void Init()
    {
        _uiControllerDic = new Dictionary<string, List<UIBehaviour>>();
        GetAllControllers();
    }
    private void GetAllControllers()
    {
        foreach (Transform trans in transform)
            AddTransControllers(trans);
    }
    private void AddTransControllers(Transform trans,string pathName="")
    {
        List<UIBehaviour> controllers;
        var name = trans.name;
        if (!CheckControllerName(name))
            return ;
        controllers = GetFitController(trans);
        if (controllers == null || controllers.Count <= 0)
            return;
        if (pathName == "")
            pathName = name;
        _uiControllerDic[pathName] = controllers;
    }
    private List<UIBehaviour> GetFitController(Transform trans)
    {
        var controllers = trans.GetComponents<UIBehaviour>().ToList();
        for (int i = controllers.Count-1; i>=0 ; i--)
        {
            var controller=controllers[i];
            if (!CheckController(controller))
                controllers.Remove(controller);
        }
        return controllers;
    }
    //检测名字是不是符合规范
    private bool CheckControllerName(string name)
    {
        foreach (var item in _fitUIControllerName)
            if (name.StartsWith(item))
                return true;
        return false;
    }
    //检测是不是对的组件
    private bool CheckController(UIBehaviour behaviour)
    {
        if (_fitUIController.Contains(behaviour.GetType()))
            return true;
        return false;
    }
    public T GetControl<T>(string name) where T:UIBehaviour
    {
        if(!_uiControllerDic.TryGetValue(name,out List<UIBehaviour> controllers))
        {
            Transform trans = transform.Find(name);
            if (trans == null)
            {
                Debug.LogError($"名为{transform.name}的物体下不存在名为:{name}的子物体");
                return null;
            }
            AddTransControllers(trans,name);
            controllers = _uiControllerDic[name];
        }
        foreach (var item in controllers)
            if (item is T)
                return item as T;
        return default(T);
    }
}