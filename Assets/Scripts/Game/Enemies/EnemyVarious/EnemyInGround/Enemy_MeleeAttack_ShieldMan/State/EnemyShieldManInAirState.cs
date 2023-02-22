using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManInAirState : EnemyBaseInAirState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    public EnemyShieldManInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDead, E_CharacterState.DEAD);
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.LAND);
        AddTargetState(() => _isInMeleeAttack, E_CharacterState.MELEEAIRATTACK);
    }
}


