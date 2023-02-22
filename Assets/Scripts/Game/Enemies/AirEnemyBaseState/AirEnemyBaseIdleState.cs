using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseIdleState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isTouchingGround;
    protected bool _isPlayerInAgro;
    protected Vector2 _curDir;
    protected float _lastChangeDirTime;//上一次翻转方向的时间
    public AirEnemyBaseIdleState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _curDir = Vector3.up;
    }
    public override void Exit()
    {
        base.Exit();
        Move.SetVelocityZero();
    }
    public override void Check()
    {
        base.Check();
        _isTouchingGround = ColliderCheck.Ground;
        _isPlayerInAgro = ColliderCheck.PlayerInMaxArgo;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckIfCanChangsState();
    }
    //Idle的时候向上飞行或者向下飞行
    protected  virtual void IdleFlyUpAndDown()
    {
        Move.SetYVelocity(_data.idleVelocity*_curDir.y);
        if (CheckCanChangeDir())
            ReverserCurY();
    }
    //Idle的时候向左或者向右飞行 
    protected virtual void IdleFlyLeftAndRight()
    {
        Move.SetXVelocity(_data.idleVelocity * _curDir.x);
        if (CheckCanChangeDir())
            ReverserCurX();
    }
    //飞行的时候完全反向
    protected virtual void IdleFlyReverseDir()
    {
        Move.SetVelocity(_data.idleVelocity,_curDir);
        if (CheckCanChangeDir())
            _curDir = _curDir.Reverse();
    }
    //翻转当前x方向
    protected virtual void ReverserCurX()
    {
        _curDir = _curDir.ReverseX();
        _lastChangeDirTime = Time.time;
        Move.Flip();
    }
    //翻转当前y方向
    protected virtual void ReverserCurY()
    {
        _curDir = _curDir.ReverseY();
        _lastChangeDirTime = Time.time;
    }
    //翻转 x y方向
    protected virtual void ReverserCur()
    {
        _curDir = _curDir.Reverse();
        _lastChangeDirTime = Time.time;
    }
    //检测是否可以改变方向
    private bool CheckCanChangeDir()
    {
        return (Time.time >= _lastChangeDirTime + _data.changeIdleDirTime || _isTouchingGround) && (Time.time >= _lastChangeDirTime + _data.minChangeDirTime);
    }
}
