using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeWaitState : AirEnemyBaseWaitState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    public AirEnemyFlyEyeWaitState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isOverCurState,E_CharacterState.CHARGE);
    }
}
