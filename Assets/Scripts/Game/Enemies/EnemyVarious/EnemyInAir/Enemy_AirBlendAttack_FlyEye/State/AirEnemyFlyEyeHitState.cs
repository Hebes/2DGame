using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeHitState : AirEnemyBaseHitState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    public AirEnemyFlyEyeHitState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.BACK);
    }
}
