using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressMeleeAttackTwoState : EnemyBaseMeleeAttackState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    private bool _playerInMaxAgro;
    private string _audiioClipName = "enemy_huntress_charge";
    public EnemyHuntressMeleeAttackTwoState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown&&_playerInMaxAgro , E_CharacterState.DODGE);
        AddTargetState(() => _isAbilityDown , E_CharacterState.DETECTED);
    }
    public override void Check()
    {
        base.Check();
        _playerInMaxAgro = ColliderCheck.PlayerInMaxArgo;
    }
    public override void Enter()
    {
        SetAttackCombatIndex(_data.meleeAttackTwoCombatIndex);
        AudioMgr.Instance.PlayOnce(_audiioClipName);
        base.Enter();
    }
    public override void AnimatorEnterTrigger()
    {
        base.AnimatorEnterTrigger();
        Move.SetXVelocity(0);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocityInFacing(_data.chargeVelocity, true);
    }
}
