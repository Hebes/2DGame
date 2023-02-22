using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurMeleeAttackState : BossBaseMeleeAttackState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private string _audioClipName = "enemy_minotaur_meleeAttack";
    public BossMinotaurMeleeAttackState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=> _isAbilityDown&&fsm.GetState<BossMinotaurMeleeAttackState>().CheckCanMeleeAttackContinue(),E_CharacterState.MELEEATTACK
            ,null,AddAttackIndex);
        AddTargetState(()=> _isAbilityDown,E_CharacterState.DETECTED);
    }
    protected override void PlayCurCombatIndexAudio()
    {
        base.PlayCurCombatIndexAudio();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
    public override void Exit()
    {
        base.Exit();
        Move.LookAtPlayer();
    }
}
