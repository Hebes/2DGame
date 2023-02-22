using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseDashState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isPlayerInMeleeAttack;
    protected bool _isWallFront;
    protected bool _isLedgeVerticalFront;


    protected bool _isDashOver;

    public EnemyBaseDashState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isDashOver = false;
    }
    public override void Check()
    {
        base.Check();
        _isWallFront = ColliderCheck.WallFront;
        _isLedgeVerticalFront = ColliderCheck.LedgeVerticalFront;
        _isPlayerInMeleeAttack = ColliderCheck.PlayerInMeleeAttackDis;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Dash();
    }
    protected virtual void Dash()
    {
        if (_isDashOver)
            return;
        Move.SetXVelocityInFacing(_data.dashVelocity,true);//冲刺的时候自己要向正面
        if ((Time.time >= _enterTime + _data.maxDashTime||_isWallFront||!_isLedgeVerticalFront))
            _isDashOver = true;
    }
    //检测是否可以冲刺
    public bool CheckCanDash()
        => Time.time >= _enterTime + _data.dashCoolDownTime;
}
