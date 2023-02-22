using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWormInAirState : EnemyBaseInAirState<Enemy_RangeAttack_FireWorm, EnemyFireWormData>
{
    public EnemyFireWormInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_RangeAttack_FireWorm ower, EnemyFireWormData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDead, E_CharacterState.DEAD);
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.MOVE);
    }
}
