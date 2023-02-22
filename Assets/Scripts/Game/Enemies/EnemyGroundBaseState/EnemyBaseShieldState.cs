/****************************************************
    文件：EnemyBaseShieldState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/16 15:6:59
	功能：在陆地上的敌人举盾状态基类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBaseShieldState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isShieldOver;

    protected bool _isWallBack;
    protected bool _isLedgeVerticalBack;
    protected bool _isOnGround;
    protected bool _isInMaxAgro;

    private float _lastFlipTime;
    private float _lastShiledStateExitTime = float.NegativeInfinity;

    private float _lastBeatTime;
    protected bool _isBeatBack;
    public EnemyBaseShieldState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _core.Get<EnemyBehaviorComponent>().SetShieldState(true);
        ShieldEnterAction();
        SubEventMgr.AddEvent(E_EventName.CHARACTER_BEATBACK, BeatBack);
    }
    public override void Exit()
    {
        base.Exit();
        _lastShiledStateExitTime = Time.time;
        _core.Get<EnemyBehaviorComponent>().SetShieldState(false);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_BEATBACK, BeatBack);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckIsShieldOver();
        CheckBeatBackForceIsOver();
        ShieldUpdateAction();
    }
    protected virtual void ShieldEnterAction()
    {
        Move.LookAtPlayer();
        Move.SetXVelocity(0);
        _isShieldOver = false;
    }
    protected virtual void ShieldUpdateAction()
    {
        ShieldMove();
        ShieldFlip();
    }
    protected virtual void ShieldMove()
    {
        if (_isBeatBack)
            return;
        if (_isWallBack || !_isLedgeVerticalBack)
        {
            return;
        }
        Move.SetXVelocityInFacing(_data.shieldMoveVelocity, false);
    }
    protected virtual void ShieldFlip()
    {
        if (Time.time >= _lastFlipTime + _data.shieldminFlipTime)
        {
            Move.LookAtPlayer();
            _lastFlipTime = Time.time;
        }
    }
    public override void Check()
    {
        base.Check();
        _isWallBack = ColliderCheck.WallBack;
        _isLedgeVerticalBack = ColliderCheck.LedgeVerticalBack;
        _isOnGround = ColliderCheck.Ground;
        _isInMaxAgro = ColliderCheck.PlayerInMaxArgo;
    }
    protected virtual void CheckIsShieldOver()
    {
        if (!_isShieldOver && Time.time >= _enterTime + _data.maxShieldTime)
            _isShieldOver = true;
    }
    public bool CheckCanShield()
        => Time.time >= _lastShiledStateExitTime + _data.shieldCoolDownTime;
    protected virtual void BeatBack(params object[] args)
    {
        Move.SetXVelocityInFacing(_data.shieldBeatBackForece, false);
        _lastBeatTime = Time.time;
        _isBeatBack = true;
    }
    //检测受到击退的力 是否完毕
    protected virtual void CheckBeatBackForceIsOver()
    {
        if (!_isBeatBack) return;
        if (Time.time >= _lastBeatTime + _data.shieldBeatBackForeceTime)
            Move.SetXVelocityInFacing(0, false);
    }
}