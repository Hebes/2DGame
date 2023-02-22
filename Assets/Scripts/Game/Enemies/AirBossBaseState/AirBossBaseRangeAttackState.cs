
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossBaseRangeAttackState<T, X> : AirEnemyBaseRangeAttackState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseRangeAttackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}
