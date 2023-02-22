using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomIdleState : EnemyBaseIdleState<Enemy_MeleeAttack_Mushroom, EnemyMushroomData>
{
    public EnemyMushroomIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Mushroom ower, EnemyMushroomData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(() => _isIdleOver, E_CharacterState.MOVE);
    }
}
