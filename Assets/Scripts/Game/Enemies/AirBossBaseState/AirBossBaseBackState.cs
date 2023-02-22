using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirBossBaseBackState<T, X> : AirEnemyBaseBackState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseBackState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {

    }
}
