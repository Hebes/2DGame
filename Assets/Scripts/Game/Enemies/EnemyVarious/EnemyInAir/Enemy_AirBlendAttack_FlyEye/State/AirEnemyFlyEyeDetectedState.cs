using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeDetectedState : AirEnemyBaseDetectedState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    public AirEnemyFlyEyeDetectedState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDetectedOver && fsm.GetState<AirEnemyFlyEyeRangeAttackState>().CheckCanEnterRangeAttack(), E_CharacterState.RANGEATTACK);
        AddTargetState(() => _isDetectedOver && _isPlayerInMaxArgo, E_CharacterState.CHARGE);
        AddTargetState(() => _isDetectedOver && !_isPlayerInMinArgo, E_CharacterState.IDLE);
    }
}
