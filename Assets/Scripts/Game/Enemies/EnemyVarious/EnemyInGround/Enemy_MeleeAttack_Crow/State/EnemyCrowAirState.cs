using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowAirState : EnemyBaseInAirState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowAirState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.LAND);
        //如果在空中超过一段时间
        AddTargetState(() => _isDead, E_CharacterState.DEAD);
    }
}
