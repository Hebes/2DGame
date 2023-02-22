using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventSystem 
{
    void AddEvent(E_EventName name,Action<object[]> events);
    void RemoveEvent(E_EventName name,Action<object[]> events);
    void ExecuteEvent(E_EventName name,object[] args=null);
}
