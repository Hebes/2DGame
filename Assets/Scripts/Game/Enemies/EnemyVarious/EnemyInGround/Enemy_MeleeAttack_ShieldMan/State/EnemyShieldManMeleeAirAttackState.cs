using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManMeleeAirAttackState : EnemyBaseMeleeAttackState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    public EnemyShieldManMeleeAirAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && _isInMeleeAttack && _fsm.GetState<EnemyShieldManMeleeAttackState>().CheckCanMeleeAttackContinue(), E_CharacterState.MELEEAIRATTACK);
        AddTargetState(() => _isAbilityDown, E_CharacterState.INAIR);
    }
}