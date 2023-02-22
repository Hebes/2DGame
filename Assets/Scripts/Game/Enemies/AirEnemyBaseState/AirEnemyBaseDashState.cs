using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseDashState<T, X> : AirEnemyAbilityState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isPlayerInMeleeAttack;
    protected bool _isGround;
    protected Vector2 _dashDir;//当前冲刺的方向
    protected bool _isDash ;//是否冲刺

    public AirEnemyBaseDashState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        ResetDashDir();
        _isDash = true;
    }
    public override void Check()
    {
        base.Check();
        _isPlayerInMeleeAttack = ColliderCheck.PlayerInMeleeAttackDis;
        _isGround = ColliderCheck.Ground;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckDashIsOver();
        Dash();
    }
    //重置冲刺方向
    protected virtual void ResetDashDir()
    {
        if (Move.PlayerTf == null)
            return;
        _dashDir = (Move.PlayerTf.position - _ower.transform.position).normalized;
    }
    //检测冲刺是否完毕
    protected void CheckDashIsOver()
    {
        if (_isDash && (Time.time >= _enterTime + _data.maxDashTime||_isGround))
            _isDash = false;
    }
    //冲刺
    protected void Dash()
    {
        if (_isDash)
            Move.SetVelocity(_data.dashVelocity, _dashDir);
    }
    //检测是否可以进入冲刺状态
    public bool CheckCanDash()
        => Time.time >= _enterTime + _data.dashCoolDownTime;
}
