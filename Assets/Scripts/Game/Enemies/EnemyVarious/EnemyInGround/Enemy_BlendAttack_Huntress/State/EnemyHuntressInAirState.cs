using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressInAirState : EnemyBaseInAirState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    public EnemyHuntressInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDead, E_CharacterState.DEAD);
        //如果在空中超过一段时间
        AddTargetState(() => _isInMeleeAttack, E_CharacterState.MELEEATTACK);
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.DETECTED);
    }
}
