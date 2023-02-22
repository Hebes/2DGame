using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Core : MonoBehaviour
{
    public SubEventMgr SubEventMgr { get; private set; }
    private Dictionary<Type, ComponentBase> _componetDic = new Dictionary<Type, ComponentBase>();
    public void Init()
    {
        SubEventMgr = this.transform.parent.AddOrGet<SubEventMgr>();
        var componentBases = transform.GetComponentsInChildren<ComponentBase>();
        foreach (var com in componentBases)
        {
            var type = com.GetType();
            _componetDic[type] = com;
            com.Init();
        }
    }
    public T Get<T>() where T:ComponentBase
    {
        var type = typeof(T);
        if (_componetDic.ContainsKey(type))
            return (T)_componetDic[type];
        Debug.LogError($"当前组件:{type.ToString()}不存在");
        return null;
    }
}
