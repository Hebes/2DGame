using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseChargeState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isWallFront;
    protected bool _isLedgeVertical;
    protected bool _isInMinAgro;//在严重警惕范围中
    protected bool _isInMaxAgro;//在警惕范围中
    protected bool _isInMeleeAttack;//在近战攻击范围内
    protected bool _isInRangeAttack;//在远程攻击范围内
    protected bool _isChargeOver;
    protected float _lastFlipTime=float.NegativeInfinity;
    public EnemyBaseChargeState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Move.SetXVelocityInFacing(_data.chargeVelocity,true);
        _isChargeOver = false;
        _lastFlipTime = 0;
    }
    public override void Check()
    {
        base.Check();
        _isInMinAgro = ColliderCheck.PlayerInMinArgo;
        _isInMaxAgro = ColliderCheck.PlayerInMaxArgo;
        _isInMeleeAttack = ColliderCheck.PlayerInMeleeAttackDis;
        _isInRangeAttack = ColliderCheck.PlayerInRangeAttackDis;
        _isWallFront = ColliderCheck.WallFront;
        _isLedgeVertical = ColliderCheck.LedgeVerticalFront;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckIsChargeOver();
        if (Time.time >= _lastFlipTime + _data.detectedMinFlipTime)
        {
            Move.LookAtPlayer();
            _lastFlipTime = Time.time;
        }
    }
    //检车是否追逐结束 如果撞到墙与遇到悬崖 或者 时间到了 则判断 追逐结束
    protected void CheckIsChargeOver()
    {
        if (_isChargeOver)
            return;
        Move.SetXVelocityInFacing(_data.chargeVelocity,true);
        if (Time.time >= _enterTime + _data.chargeTime)
            _isChargeOver = true;
        if (_isWallFront || !_isLedgeVertical)
            _isChargeOver = true;
    }
}
