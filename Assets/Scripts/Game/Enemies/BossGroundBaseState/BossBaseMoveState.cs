using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseMoveState<T, X> : EnemyBaseMoveState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseMoveState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}
