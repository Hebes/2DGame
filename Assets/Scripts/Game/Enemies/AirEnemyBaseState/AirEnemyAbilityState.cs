using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AirEnemyAbilityState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isAbilityDown;
    public AirEnemyAbilityState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
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
