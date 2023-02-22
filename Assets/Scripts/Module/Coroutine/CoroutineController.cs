using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineController
{
    private static int _id;
    private MonoBehaviour _mono;//开启协程的Mono类
    private IEnumerator _routine;
    public int Id { get; private set; }
    private Coroutine _coroutine;
    public CoroutineController(IEnumerator routine,MonoBehaviour mono)
    {
        _routine = routine;
        _mono = mono;
        Id = GetId();
    }
    public void Start()
        => _coroutine = _mono?.StartCoroutine(_routine);
    //重新开启当前协程
    public void Restart()
    {
        Stop();
        Start();
    }
    public void Stop()
    {
        if (_coroutine == null)
        {
            Debug.LogError($"当前没有开启的协程");
            return;
        }
        _mono?.StopCoroutine(_coroutine);
    }
    //因为int类型的默认值为0 所以controller的id最好才能从1开始
    public static int GetId()
    {
        _id++;
        if (_id == int.MaxValue)
            _id = 1;
        return _id;
    }
}
