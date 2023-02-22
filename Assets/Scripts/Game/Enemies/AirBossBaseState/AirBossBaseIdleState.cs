using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossBaseIdleState<T, X> : AirEnemyBaseIdleState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseIdleState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}
