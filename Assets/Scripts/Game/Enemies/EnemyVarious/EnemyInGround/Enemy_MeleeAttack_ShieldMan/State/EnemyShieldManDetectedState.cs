using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldManDetectedState : EnemyBaseDetectedState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    protected bool _playerInAir;
    protected bool _bulletInMeleeAttackDis;
    public EnemyShieldManDetectedState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => (_isInMeleeAttack || _bulletInMeleeAttackDis)
        &&fsm.GetState<EnemyShieldManMeleeAttackState>().CheckCanMeleeAttack(), E_CharacterState.MELEEATTACK);
        AddTargetState(() => _playerInAir, E_CharacterState.DODGE);
        AddTargetState(() => _inOverMaxWaitTime && _isInMaxAgro && fsm.GetState<EnemyShieldManDashState>().CheckCanDash(), E_CharacterState.DASH);
        AddTargetState(() => _inOverMaxWaitTime && _isInMaxAgro, E_CharacterState.CHARGE);
        AddTargetState(() => _inOverMaxWaitTime, E_CharacterState.LOOKFOR);
    }
    public override void Check()
    {
        base.Check();
        _playerInAir = ColliderCheck.PlayerInAir;
        _bulletInMeleeAttackDis = ColliderCheck.BulletInMeleeAttackDis;
    }
}
