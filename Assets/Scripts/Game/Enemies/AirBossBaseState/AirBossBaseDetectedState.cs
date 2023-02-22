using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AirBossBaseDetectedState<T, X> : AirEnemyBaseDetectedState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseDetectedState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}
