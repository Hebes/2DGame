using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeBackState : AirEnemyBaseBackState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    public AirEnemyFlyEyeBackState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isBackOver,E_CharacterState.CHARGE);
    }
}
