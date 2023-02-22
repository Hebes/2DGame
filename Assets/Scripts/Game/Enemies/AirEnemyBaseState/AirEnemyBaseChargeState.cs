using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseChargeState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isChargeOver;
    protected bool _isInMeleeAttackDis;
    protected bool _isInRangeAttackDis;
    protected bool _isGround;
    protected bool _isPlayerInMaxArgo;
    protected bool _isBulletInMeleeAttackDis;



    protected bool _canFlip;
    protected float _lastFlipTime;
    protected bool _isEnterWaitState;//是否可以进入等待状态
    protected float _lastNotTouchingWallTime;//最后不结束墙的时间
    protected float _lastPlayerInMaxArgoTime;
    public AirEnemyBaseChargeState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isChargeOver = false;
        _isEnterWaitState = false;
        NavFindingComponent.SetStopDis(_data.chargeStopDis);
        NavFindingComponent.SetCurVelocity(_data.chargeVelocity);
    }
    public override void Exit()
    {
        base.Exit();
        //停止寻路
        NavFindingComponent.StopFindPath();
    }
    public override void Check()
    {
        base.Check();
        _isInMeleeAttackDis = ColliderCheck.PlayerInMeleeAttackDis;
        _isGround = ColliderCheck.Ground;
        _isPlayerInMaxArgo = ColliderCheck.PlayerInMaxArgo;
        _isInRangeAttackDis = ColliderCheck.PlayerInRangeAttackDis;
        _isBulletInMeleeAttackDis = ColliderCheck.BulletInMeleeAttackDis;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckCanFlip();
        LookAtPlayer();
        CheckChargeIsOver();
        CheckIsEnterWaitState();
        if(Move.PlayerTf!=null)
            NavFindingComponent.SetDestination(Move.PlayerTf.position);
    }
    //检测是否追逐完毕
    private void CheckChargeIsOver()
    {
        if(_isPlayerInMaxArgo)
        {
            _isChargeOver = false;
            _lastPlayerInMaxArgoTime = Time.time;
            return;
        }
        if (!_isChargeOver && Time.time >= _lastPlayerInMaxArgoTime + _data.chargeTime)
            _isChargeOver = true;
    }
    protected void LookAtPlayer()
    {
        if (_canFlip)
        {
            Move.LookAtPlayer();
            _canFlip = false;
            _lastFlipTime = Time.time;
        }
    }
    //检测是否可以翻转
    private void CheckCanFlip()
    {
        if (!_canFlip && Time.time >= _lastFlipTime + _data.chargeMinFlipTime)
            _canFlip = true;
    }
    //检测是否可以进入等待状态
    private void CheckIsEnterWaitState()
    {
        if (!_isGround)
        {
            _lastNotTouchingWallTime = Time.time;
            _isEnterWaitState = false;
            return;
        }
        if (!_isEnterWaitState && Time.time >= _lastNotTouchingWallTime + _data.maxTouchingWallTime)
            _isEnterWaitState = true;
    }
    
}
