using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeInAirState : EnemyBaseInAirState<Enemy_MeleeAttack_Slime, EnemySlimeData>
{
    public EnemySlimeInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Slime ower, EnemySlimeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isDead,E_CharacterState.DEAD);
        AddTargetState(()=>_isOnGround&&Move.GetCurVelocity.y<=0.01f,E_CharacterState.MOVE);
    }
}
