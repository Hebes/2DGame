using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullMeleeAirAttackState : EnemyBaseMeleeAttackState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    private string _audioClipName = "enemy_skull_meleeAttack";
    public EnemySkullMeleeAirAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && _isInMeleeAttack && _fsm.GetState<EnemySkullMeleeAttackState>().CheckCanMeleeAttackContinue(), E_CharacterState.MELEEAIRATTACK);
        AddTargetState(()=>_isAbilityDown,E_CharacterState.INAIR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
}
