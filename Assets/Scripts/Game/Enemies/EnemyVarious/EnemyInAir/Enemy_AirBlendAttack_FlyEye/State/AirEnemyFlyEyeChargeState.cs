using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeChargeState : AirEnemyBaseChargeState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    private float _lastAttackTime;
    
    public AirEnemyFlyEyeChargeState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        var meleeAttackState = fsm.GetState<AirEnemyFlyEyeMeleeAttackState>();
        var dashState = fsm.GetState<AirEnemyFlyEyeDashState>();
        var rangeAttackState = fsm.GetState<AirEnemyFlyEyeRangeAttackState>();
        var waitState = fsm.GetState<AirEnemyFlyEyeWaitState>();
        AddTargetState(() => (_isInMeleeAttackDis||_isBulletInMeleeAttackDis) && meleeAttackState.CheckCanMeleeAttack(), E_CharacterState.MELEEATTACK,RecordAttackTime);
        AddTargetState(() => CheckCanAttack() && dashState.CheckCanDash(), E_CharacterState.DASH,RecordAttackTime);
        AddTargetState(() => CheckCanAttack()&&_isInRangeAttackDis && rangeAttackState.CheckCanEnterRangeAttack(),E_CharacterState.RANGEATTACK,RecordAttackTime);
        AddTargetState(() => _isEnterWaitState && waitState.CheckCanEnterWaitState(), E_CharacterState.WAIT);
        AddTargetState(()=>_isChargeOver,E_CharacterState.DETECTED);
    }
    private bool CheckCanAttack()
        => Time.time >= _lastAttackTime + _data.changeAttackOffsetTime;
    private void RecordAttackTime()
        => _lastAttackTime = Time.time;
}
