using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeIdleState : AirEnemyBaseIdleState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    public AirEnemyFlyEyeIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isPlayerInAgro, E_CharacterState.DETECTED);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        IdleFlyUpAndDown();
    }
}
