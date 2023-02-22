using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressMoveState : EnemyBaseMoveState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    public EnemyHuntressMoveState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(() => _isInMinAgro||_playerInBack, E_CharacterState.DETECTED);
        AddTargetState(() => _isWallFront || !_isLedgeVerticalFront, E_CharacterState.IDLE);
    }
}
