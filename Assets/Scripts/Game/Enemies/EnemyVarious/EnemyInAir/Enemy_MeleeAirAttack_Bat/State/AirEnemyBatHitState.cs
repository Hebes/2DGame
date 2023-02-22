using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBatHitState : AirEnemyBaseHitState<Enemy_MeleeAirAttack_Bat, AirEnemyBatData>
{
    public AirEnemyBatHitState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAirAttack_Bat ower, AirEnemyBatData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.IDLE);
    }


}
