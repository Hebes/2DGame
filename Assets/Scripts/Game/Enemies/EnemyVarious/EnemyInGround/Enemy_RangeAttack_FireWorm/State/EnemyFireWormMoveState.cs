using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWormMoveState : EnemyBaseMoveState<Enemy_RangeAttack_FireWorm, EnemyFireWormData>
{
    public EnemyFireWormMoveState(FiniteStateMachine fsm, string animBoolName, Enemy_RangeAttack_FireWorm ower, EnemyFireWormData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(() => _playerInBack || _isInMinAgro, E_CharacterState.DETECTED);
        AddTargetState(()=>_isWallFront||!_isLedgeVerticalFront,E_CharacterState.IDLE);
    }
}
