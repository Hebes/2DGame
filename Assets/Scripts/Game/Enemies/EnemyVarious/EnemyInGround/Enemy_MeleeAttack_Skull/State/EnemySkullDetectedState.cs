using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullDetectedState : EnemyBaseDetectedState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    protected bool _playerInAir;
    protected bool _bulletInMeleeAttackDis;
    public EnemySkullDetectedState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>(_isInMeleeAttack || _bulletInMeleeAttackDis)
        &&fsm.GetState<EnemySkullMeleeAttackState>().CheckCanMeleeAttack(), E_CharacterState.MELEEATTACK);
        AddTargetState(()=>_playerInAir,E_CharacterState.DODGE);
        AddTargetState(()=>_inOverMaxWaitTime && _isInMaxAgro&&fsm.GetState<EnemySkullDashState>().CheckCanDash(),E_CharacterState.DASH);
        AddTargetState(()=>_inOverMaxWaitTime&&_isInMaxAgro,E_CharacterState.CHARGE);
        AddTargetState(()=>_inOverMaxWaitTime,E_CharacterState.MOVE);
    }
    public override void Check()
    {
        base.Check();
        _playerInAir = ColliderCheck.PlayerInAir;
        _bulletInMeleeAttackDis = ColliderCheck.BulletInMeleeAttackDis;
    }
}
