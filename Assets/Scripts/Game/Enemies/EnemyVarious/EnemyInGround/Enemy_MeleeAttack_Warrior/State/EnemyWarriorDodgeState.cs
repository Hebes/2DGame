using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyWarriorDodgeState : EnemyBaseDodgeState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    private string _audioClipName = "enemy_warrior_jump";
    public EnemyWarriorDodgeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown, E_CharacterState.INAIR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
