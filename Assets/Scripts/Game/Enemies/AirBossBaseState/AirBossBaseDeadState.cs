using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossBaseDeadState<T, X> : AirEnemyBaseDeadState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseDeadState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}
