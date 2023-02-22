using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : IEventSystem
{
    private Dictionary<E_EventName, EventData<object[]>> _eventDic = new Dictionary<E_EventName, EventData<object[]>>();
    public void AddEvent(E_EventName name, Action<object[]> newEvent)
    {
        if (!_eventDic.ContainsKey(name))
            _eventDic[name] = new EventData<object[]>();
        if (!_eventDic[name].AddEvent(newEvent))
            Debug.LogError($"添加事件:{name}失败,该事件已经添加 或者 委托参数不匹配");
    }
    public void RemoveEvent(E_EventName name, Action<object[]> events)
    {
        if (_eventDic.ContainsKey(name))
            if (!_eventDic[name].RemoveEvent(events))
                Debug.LogError($"移除事件:{name}失败,该事件未被添加");
    }
    public void ExecuteEvent(E_EventName name, object[] args = null)
    {
        if (_eventDic.ContainsKey(name))
            _eventDic[name].ExecuteEvent(args);
        //else
        //    Debug.LogError($"事件名为：{name}的事件不存在");
    }
}
