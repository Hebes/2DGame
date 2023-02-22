using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowMoveState : EnemyBaseMoveState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowMoveState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(ChangeCanIntoDetectedState,E_CharacterState.DETECTED);
        AddTargetState(() => _isWallFront || !_isLedgeVerticalFront, E_CharacterState.IDLE);
    }
    private bool ChangeCanIntoDetectedState()
    {
        //如果检测到主角在背后
        if (_playerInBack)
            return true;
        //如果主角在最小警惕范围内
        if (_isInMinAgro)
            return true;
        return false;
    }
}
