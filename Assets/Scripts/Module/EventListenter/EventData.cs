using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventData<T> 
{
    private HashSet<Action<T>> _actionTable=new HashSet<Action<T>>();
    private Action<T> _callback;
    public bool AddEvent(Action<T> action)
    {
        if (_actionTable.Add(action))
        {
            _callback += action;
            return true;
        }
        return false;
    }
    public bool RemoveEvent(Action<T> action)
    {
        if (_actionTable.Remove(action))
        {
            _callback -= action;
            return true;
        }
        return false;
    }
    public void ExecuteEvent(T args) => _callback?.Invoke(args);
}
