using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressBackState : EnemyBaseBackState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    private string _audiioClipName="enemy_huntress_back";
    public EnemyHuntressBackState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isInMeleeAttack, E_CharacterState.MELEEATTACK);
        AddTargetState(() => _isBackOver, E_CharacterState.DODGE);
    }
    public override void Enter()
    {
        base.Enter();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_INVINCIBLE);
        AudioMgr.Instance.PlayOnce(_audiioClipName);
    }
    public override void Exit()
    {
        base.Exit();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_STOPINVINCIBLE);
    }
}
