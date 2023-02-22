using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseHitState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected Vector2 _hitDir;
    public AirEnemyBaseHitState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _hitDir = (_ower.transform.position - Move.PlayerTf.position).normalized;
        Move.SetVelocity(Behavior._strength, _hitDir);
    }
    public override void Exit()
    {
        base.Exit();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_HITOVER);
    }
}
