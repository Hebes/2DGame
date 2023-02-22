using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressMeleeAttackState : EnemyBaseMeleeAttackState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    private bool _grounded;
    private string _audioClipName = "enemy_huntress_meleeAttack";
    public EnemyHuntressMeleeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && _fsm.GetState<EnemyHuntressMeleeAttackState>().CheckCanMeleeAttackContinue(), E_CharacterState.MELEEATTACK
         , null, AddAttackIndex);
        AddTargetState(() => _isAbilityDown && !_grounded, E_CharacterState.INAIR);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
    }
    protected override void PlayCurCombatIndexAudio()
    {
        base.PlayCurCombatIndexAudio();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
    public override void Check()
    {
        base.Check();
        _grounded = ColliderCheck.Ground;
    }
}
