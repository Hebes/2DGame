using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManMoveState : EnemyBaseMoveState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    public EnemyShieldManMoveState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isWallFront || !_isLedgeVerticalFront , E_CharacterState.IDLE);
        AddTargetState(() =>_isInMinAgro||_playerInBack , E_CharacterState.DETECTED);
    }
}
