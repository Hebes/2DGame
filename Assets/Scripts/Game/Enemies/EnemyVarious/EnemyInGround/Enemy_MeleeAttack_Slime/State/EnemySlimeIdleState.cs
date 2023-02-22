using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySlimeIdleState : EnemyBaseIdleState<Enemy_MeleeAttack_Slime, EnemySlimeData>
{
    public EnemySlimeIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Slime ower, EnemySlimeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(() => _isIdleOver, E_CharacterState.MOVE);
    }
}
