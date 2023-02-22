using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeRangeAttackState : AirEnemyBaseRangeAttackState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    private string _auidoClioName = "enemy_flyeye_rangeAttack";
    public AirEnemyFlyEyeRangeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown&&CheckCanEnterRangeAttack(), E_CharacterState.RANGEATTACK);
        AddTargetState(() => _isAbilityDown, E_CharacterState.CHARGE);
    }
    protected override void SetAbilityDown()
    {
        base.SetAbilityDown();
        AnimFinish = true;
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
    protected override Vector3 GetSpawnBulletDir()
        => (PlayerPos - _ower.transform.position).normalized;
}
