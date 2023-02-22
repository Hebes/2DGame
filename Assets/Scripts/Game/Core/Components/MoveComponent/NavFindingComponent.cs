using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;
public class NavFindingComponent : ComponentBase
{
    [BoxGroup("设置目标点的冷却时间"), SerializeField]
    protected float _setDesCoolDownTime = 1f;
    private float _lastSetDesTime;
    protected NavMeshAgent _agent;
    public override void Init()
    {
        base.Init();
        _agent = _owerTf.GetComponent<NavMeshAgent>();
        if (_agent == null)
            Debug.LogError($"名字为:{_owerTf.name}的物体身上没有飞行怪物身上没有代理组件");
        //插件使用手册上必须满条件
        _agent.updateRotation = false;//不同步代理的旋转
        _agent.updateUpAxis = false;
        //_agent.autoRepath = false;
        SetActiveAgent(false);
    }
    private void OnDisable()
    {
        _lastSetDesTime = 0;
    }
    //设置最大速度 只有在Start中设置才有效
    public void SetMaxSpeed(float speed)
        => _agent.speed = speed;
    //设置最大加速度
    public void SetMaxAccleration(float accleration)
        => _agent.acceleration = accleration;
    //设置速度   
    public void SetCurVelocity(float speed)
    {
        SetMaxSpeed(speed);//设置最大速度
        SetMaxAccleration(speed / Consts.CHARACTER_ACCELERATION_RATIO);//设置加速度
        _agent.velocity = _agent.desiredVelocity * speed;
    }
    //下面这样设置速度 好像不太对
    //设置终点
    public bool SetDestination(Vector3 destination)
    {
        if (Time.time < _lastSetDesTime + _setDesCoolDownTime)
            return false;
        //如果不在导航网格上
        SetActiveAgent(true);//先激活在判断是否在网格上
        if (!_agent.isOnNavMesh)
            return false;
        _lastSetDesTime = Time.time;
        return _agent.SetDestination(destination);
    }
    //调整代理至指定位置 如果成功返回True
    public bool Warp(Vector3 pos)
        => _agent.Warp(pos);
    //停止寻路
    public void StopFindPath()
    {
        if (_agent.hasPath)
        {
            _agent.ResetPath();
            SetCurVelocity(0);
        }
        SetActiveAgent(false);
        _agent.speed = 0;
        _agent.acceleration = 0;
        _agent.velocity = Vector2.zero;
    }
    //是否有路径
    public bool HasPath(bool value)
        => _agent.hasPath;
    //设置代理的显隐
    protected void SetActiveAgent(bool value)
    {
        if (_agent.enabled != value)
        {
            //只能在为enabled为True的情况下设置IsStopped
            if (_agent.enabled && _agent.hasPath)
                _agent.isStopped = !value;
            _agent.enabled = value;
        }
    }
    public Vector2 GetCurVelocity()
        => _agent.velocity;
    public void SetStopDis(float stopDis)
        => _agent.stoppingDistance = stopDis;
}
