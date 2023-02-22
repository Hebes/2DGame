using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseInAirState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isOnGround;
    protected bool _isDead;
    protected bool _isInMeleeAttack;
    protected bool _isInRangeAttack;
    public EnemyBaseInAirState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {
       
    }
    public override void Enter()
    {
        base.Enter();
        _isDead = false;
    }
    public override void Check()
    {
        base.Check();
        _isOnGround = ColliderCheck.Ground;
        _isInMeleeAttack = ColliderCheck.PlayerInMeleeAttackDis;
        _isInRangeAttack = ColliderCheck.PlayerInRangeAttackDis;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckIfDead();
        _anim.SetFloat(Consts.CHARACTER_ANM_YVleocity,Move.GetCurVelocity.y);
    }
    private void CheckIfDead()
    {
        if (!_isDead && Time.time >= _enterTime + _data.inAirDeadTime)
        {
            _isDead = true;
            _ower.CreateCoin();
        }
    }
}
