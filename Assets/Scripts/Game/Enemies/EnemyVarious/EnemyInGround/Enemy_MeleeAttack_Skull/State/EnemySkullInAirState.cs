using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullInAirState : EnemyBaseInAirState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    public EnemySkullInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDead, E_CharacterState.DEAD);
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.LAND);
        AddTargetState(() => _isInMeleeAttack, E_CharacterState.MELEEAIRATTACK);
        //如果在空中超过一段时间
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.LookAtPlayer();
    }
}
