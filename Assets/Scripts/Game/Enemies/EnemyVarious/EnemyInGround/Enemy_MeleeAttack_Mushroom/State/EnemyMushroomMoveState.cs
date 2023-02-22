using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomMoveState : EnemyBaseMoveState<Enemy_MeleeAttack_Mushroom, EnemyMushroomData>
{
    public EnemyMushroomMoveState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Mushroom ower, EnemyMushroomData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(()=>_isWallFront||!_isLedgeVerticalFront,E_CharacterState.IDLE);
    }
  

}
