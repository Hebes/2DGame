using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseWaitState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isOverCurState;
    protected float _lastWaitOverTime=float.NegativeInfinity;
    public AirEnemyBaseWaitState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Move.SetVelocityZero();
        _isOverCurState = false;
    }
    public override void Exit()
    {
        base.Exit();
        _lastWaitOverTime = Time.time;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetVelocityZero();
        CheckIsOver();
    }
    //判断是否可以退出当前状态
    private void CheckIsOver()
    {
        if (!_isOverCurState && Time.time >= _data.maxWaitTime)
            _isOverCurState = true;
    }
    //检测是否可以进入等待状态
    public bool CheckCanEnterWaitState()
        => Time.time >= _lastWaitOverTime + _data.waitStateCoolTime;
}
