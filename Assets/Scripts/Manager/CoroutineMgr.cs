using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineMgr : MonoSingleTon<CoroutineMgr>
{
    //需要反复使用的协程
    public Dictionary<int, CoroutineController> _routineDic = new Dictionary<int, CoroutineController>();
    //只开启一次的协程
    public Dictionary<int, CoroutineController> _onceRoutineDic = new Dictionary<int, CoroutineController>();

    //执行需要重复开启的协程
    public int Excute(IEnumerator routine,bool isAutoStart=true)
    {
        var controller = new CoroutineController(routine,this);
        int id =controller.Id;
        _routineDic[id] = controller;
        if(isAutoStart)
        controller.Start();
        return id;
    }
    //执行开启一次的协程
    public int ExcuteOne(IEnumerator routine)
    {
        var controller = new CoroutineController(routine, this);
        int id = controller.Id;
        _onceRoutineDic[id] = controller;
        controller.Start();
        return id;
    }
    //得到控制的协程的管理器
    private CoroutineController GetController(int id)
    {
        //id都是唯一的
        if (_routineDic.ContainsKey(id))
            return _routineDic[id];
        else if(_onceRoutineDic.ContainsKey(id))
            return _onceRoutineDic[id];
        else
            Debug.LogError($"id为{id}的controller不存在");
        return null;
    }
    //判断是否有当前id的协程控制器存在
    public bool HasId(int id)
    {
        foreach (var key in _onceRoutineDic.Keys)
            if (id == key)
                return true;
        foreach (var key in _routineDic.Keys)
            if (id == key)
                return true;
        return false;
    }
    //开启指定Id的协程
    public void StartIt(int id)
    {
        var controller = GetController(id);
        controller?.Start();
    }
    //停止指定的id的协程
    public void Stop(int id)
    {
        var controller = GetController(id);
        controller?.Stop();
        //如果只执行一次的话
        if (_onceRoutineDic.ContainsKey(id))
            _onceRoutineDic.Remove(id);
    }
    //重新开启指定id的协程
    public void Restart(int id)
    {
        var controller = GetController(id);
        controller?.Restart();
    }
    //通过协程实现的延迟函数 该延迟函数会受到时间缩放的影响
    public void Delay(float time, Action action)
        => ExcuteOne(Wait(time, action));
    private IEnumerator Wait(float time,Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
