using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AirBossBaseMeleeAttackState<T, X> : AirEnemyBaseMeleeAttackState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseMeleeAttackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}
