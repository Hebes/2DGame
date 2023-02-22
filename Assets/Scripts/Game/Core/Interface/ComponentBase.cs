using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentBase : MonoBehaviour
{
    public Core _core { get; protected set; }
    protected Transform _owerTf { get; private set; }
    protected SubEventMgr SubEventMgr { get; private set; }
    public virtual void Init()
    {
        _core = transform.parent.GetComponent<Core>();
        if(_core==null)
            Debug.LogError($"当前组件:{transform.name}的父物体没有Core组件");
        _owerTf = _core.transform.parent;
        if (_owerTf == null)
            Debug.LogError($"当前组件:{transform.name}的核心组件没有父物体");
        SubEventMgr = _core.SubEventMgr;
    }
    protected X Get<X>() where X : Component
    {
        X component = GetComponent<X>();
        if (component == null)
            Debug.LogError($"当前Bomb物体:{name}身上不存在该组件:{typeof(X)}");
        return component;
    }
    protected X Get<X>(string path) where X : Component
    {
        X component = transform.Find(path).GetComponent<X>();
        if (component == null)
            Debug.LogError($"当前Bomb物体:{name}的:{path}路径下为未找到该组件:{typeof(X)}");
        return component;
    }
}
