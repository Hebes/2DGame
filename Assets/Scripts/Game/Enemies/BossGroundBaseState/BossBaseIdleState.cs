using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBaseIdleState<T, X> : EnemyBaseIdleState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseIdleState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    protected override void IdleEnterAction()
    {
        CreateIdleTime();
        Move.SetXVelocity(0);
    }
    protected override void IdleExitAction()
    {

    }
}
