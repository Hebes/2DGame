using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomInAirState : EnemyBaseInAirState<Enemy_MeleeAttack_Mushroom, EnemyMushroomData>
{
    public EnemyMushroomInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Mushroom ower, EnemyMushroomData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDead, E_CharacterState.DEAD);
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.MOVE);
    }
}
