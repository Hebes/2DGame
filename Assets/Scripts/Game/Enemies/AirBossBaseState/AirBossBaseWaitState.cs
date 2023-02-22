using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossBaseWaitState<T, X> : AirEnemyBaseWaitState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseWaitState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}
