using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWormRangeAttackState : EnemyBaseRangeAttackState<Enemy_RangeAttack_FireWorm, EnemyFireWormData>
{
    private string _auidoClioName = "enemy_fireworm_rangeAttack";
    public EnemyFireWormRangeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_RangeAttack_FireWorm ower, EnemyFireWormData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && CheckCanEnterRangeAttack(), E_CharacterState.RANGEATTACK);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
    }
    protected override void SpawnBullet()
    {
        base.SpawnBullet();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
}
