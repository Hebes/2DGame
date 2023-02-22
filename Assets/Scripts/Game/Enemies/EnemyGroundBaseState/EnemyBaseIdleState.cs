using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseIdleState<T,X> : EnemyBaseState<T,X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected float _curIdleTime;


    protected bool _isInMinAgro;//在严重警惕范围中
    protected bool _playerInBack;//主角是否在背后被发现


    protected bool _isIdleOverFilp;//是否在Idle结束后进行转向
    protected bool _isIdleOver;//是否就是Idle状态
    protected bool _isGrounded;
    public EnemyBaseIdleState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        IdleEnterAction();
    }
    protected virtual void IdleEnterAction()
    {
        _isIdleOver = false;
        _isIdleOverFilp = true;
        CreateIdleTime();
        //设置x方向的速度为0
        Move.SetXVelocity(0);
    }
    public override void Check()
    {
        base.Check();
        _isInMinAgro = ColliderCheck.PlayerInMinArgo;
        _playerInBack = ColliderCheck.PlayerInBack;
        _isGrounded = ColliderCheck.Ground;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocity(0);
        CheckIdleTimeIsOver();
    }
    public override void Exit()
    {
        base.Exit();
        IdleExitAction();
    }
    protected virtual void IdleExitAction()
    {
        if (_isIdleOverFilp)
            Move.Flip();
    }
    //产生Idle时间
    protected  void CreateIdleTime() 
    => _curIdleTime = Random.Range(_data.minIdleTime, _data.maxIdleTime);
    //设置是不是在Idle结束后进行转向
    public void SetIsIdleOverFlip(bool value = true) => _isIdleOverFilp = value;
    protected void CheckIdleTimeIsOver()
    {
        if (!_isIdleOver&&Time.time >= _enterTime + _curIdleTime)
            _isIdleOver = true;
    }
}
