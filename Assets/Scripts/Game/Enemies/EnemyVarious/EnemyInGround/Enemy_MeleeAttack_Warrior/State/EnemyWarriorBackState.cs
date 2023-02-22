using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorBackState : EnemyBaseBackState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    private string _audioClipName = "enemy_warrior_charge_back";
    public EnemyWarriorBackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isBackOver, E_CharacterState.DETECTED);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
