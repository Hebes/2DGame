using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressDetectedState : EnemyBaseDetectedState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    protected bool _playerInAir;
    protected bool _bulletInMeleeAttackDis;
    public EnemyHuntressDetectedState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => (_isInMeleeAttack || _bulletInMeleeAttackDis)
       && fsm.GetState<EnemyHuntressMeleeAttackState>().CheckCanMeleeAttack(), E_CharacterState.MELEEATTACK);
        AddTargetState(()=>_isInRangeAttack&&fsm.GetState<EnemyHuntressRangeAttackState>().CheckCanEnterRangeAttack(),E_CharacterState.RANGEATTACK);
        AddTargetState(() => _playerInAir, E_CharacterState.DODGE);
        AddTargetState(() => _inOverMaxWaitTime&&_isInMaxAgro, E_CharacterState.MELEEATTACKTWO);
        AddTargetState(() => _inOverMaxWaitTime, E_CharacterState.LOOKFOR);
    }
    public override void Check()
    {
        base.Check();
        _playerInAir = ColliderCheck.PlayerInAir;
        _bulletInMeleeAttackDis = ColliderCheck.BulletInMeleeAttackDis;
    }
}
