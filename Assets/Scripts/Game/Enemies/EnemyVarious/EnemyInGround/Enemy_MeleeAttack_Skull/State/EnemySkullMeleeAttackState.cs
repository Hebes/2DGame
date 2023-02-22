using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullMeleeAttackState : EnemyBaseMeleeAttackState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    private string _audioClipName = "enemy_skull_meleeAttack";
    public EnemySkullMeleeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && _fsm.GetState<EnemySkullMeleeAttackState>().CheckCanMeleeAttackContinue(), E_CharacterState.MELEEATTACK
             , null, AddAttackIndex);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
    }
    protected override void PlayCurCombatIndexAudio()
    {
        base.PlayCurCombatIndexAudio();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
}
