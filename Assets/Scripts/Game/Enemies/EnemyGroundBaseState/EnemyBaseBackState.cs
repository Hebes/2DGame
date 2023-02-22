using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseBackState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isBackOver;
    protected bool _isWallBack;
    protected bool _isLedgeVerticalBack;
    protected bool _isInMeleeAttack;
    protected float _lastFlipTime = float.NegativeInfinity;
    public EnemyBaseBackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isBackOver = false;
        _lastFlipTime = 0;
    }
    public override void Check()
    {
        base.Check();
        _isWallBack = ColliderCheck.WallBack;
        _isLedgeVerticalBack = ColliderCheck.LedgeVerticalBack;
        _isInMeleeAttack = ColliderCheck.PlayerInMeleeAttackDis;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        BackActionUpdate();
    }
    protected virtual void  BackActionUpdate()
    {
        if (Time.time >= _lastFlipTime + _data.detectedMinFlipTime)
        {
            Move.LookAtPlayer();
            _lastFlipTime = Time.time;
        }
        CheckIfBackOver();
    }
    protected virtual void CheckIfBackOver()
    {
        if (_isBackOver)
            return;
        Move.SetXVelocityInFacing(_data.backMovementVelocity, false);
        if (Time.time >= _enterTime + _data.backTime||_isWallBack||!_isLedgeVerticalBack)
            _isBackOver = true;
    }
}
