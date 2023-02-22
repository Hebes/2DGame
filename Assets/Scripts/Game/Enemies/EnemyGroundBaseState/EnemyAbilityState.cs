using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbilityState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isAbilityDown;
    public EnemyAbilityState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isAbilityDown = false;
    }
    public override void Exit()
    {
        base.Exit();
        _isAbilityDown = true;
    }

    protected virtual void SetAbilityDown() => _isAbilityDown = true;

}
