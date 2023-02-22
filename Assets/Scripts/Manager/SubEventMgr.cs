using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEventMgr : MonoBehaviour,IEventSystem
{
    private IEventSystem _eventSystem=new EventSystem();
    public void AddEvent(E_EventName name, Action<object[]> events)
        => _eventSystem.AddEvent(name, events);
    public void ExecuteEvent(E_EventName name,params object[] args)
        => _eventSystem.ExecuteEvent(name, args);
    public void RemoveEvent(E_EventName name, Action<object[]> events)
        => _eventSystem.RemoveEvent(name, events);
}
