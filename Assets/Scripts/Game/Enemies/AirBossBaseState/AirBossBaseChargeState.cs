using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossBaseChargeState<T, X> : AirEnemyBaseChargeState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseChargeState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {


    }
}
