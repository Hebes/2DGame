using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeMoveState : EnemyBaseMoveState<Enemy_MeleeAttack_Slime, EnemySlimeData>
{
    public EnemySlimeMoveState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Slime ower, EnemySlimeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>!_isGrounded,E_CharacterState.INAIR);
        AddTargetState(()=>_isWallFront||!_isLedgeVerticalFront||CheckIsMoveOver(),E_CharacterState.IDLE);
    }
    //¼ì²âÊÇ·ñÒÆ¶¯½áÊø
    private bool CheckIsMoveOver()
        => Time.time >= _enterTime + _data.maxMoveTime;
}
