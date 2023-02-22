using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWarriorMeleeAttackState : EnemyBaseMeleeAttackState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    private string _audioClipName = "enemy_warrior_meleeAttack";
    public EnemyWarriorMeleeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && _fsm.GetState<EnemyWarriorMeleeAttackState>().CheckCanMeleeAttackContinue(), E_CharacterState.MELEEATTACK
            , null, AddAttackIndex);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
    }
    protected override void PlayCurCombatIndexAudio()
    {
        base.PlayCurCombatIndexAudio();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.LookAtPlayer();
    }
}
