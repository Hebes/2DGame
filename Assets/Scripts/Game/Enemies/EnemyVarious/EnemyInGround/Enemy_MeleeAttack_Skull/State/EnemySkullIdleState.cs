using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullIdleState : EnemyBaseIdleState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    public EnemySkullIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>!_isGrounded,E_CharacterState.INAIR);
        AddTargetState(()=>_isInMinAgro,E_CharacterState.DETECTED);
        AddTargetState(()=>_isIdleOver,E_CharacterState.MOVE);
    }
}
