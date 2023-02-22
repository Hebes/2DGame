using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManMeleeAttackState : EnemyBaseMeleeAttackState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    private string _audioClipName = "enemy_shieldMan_meleeAttack";
    public EnemyShieldManMeleeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && _fsm.GetState<EnemyShieldManMeleeAttackState>().CheckCanMeleeAttackContinue(), E_CharacterState.MELEEATTACK
          , null, AddAttackIndex);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
    }
    protected override void PlayCurCombatIndexAudio()
    {
        base.PlayCurCombatIndexAudio();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
}
