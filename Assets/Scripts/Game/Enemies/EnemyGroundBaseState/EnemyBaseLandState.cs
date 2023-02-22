using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseLandState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public EnemyBaseLandState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Move.SetVelocityZero();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
