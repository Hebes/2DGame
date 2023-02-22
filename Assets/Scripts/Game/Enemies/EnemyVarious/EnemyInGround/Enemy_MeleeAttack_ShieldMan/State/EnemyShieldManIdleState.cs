using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyShieldManIdleState : EnemyBaseIdleState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    public EnemyShieldManIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isIdleOver,E_CharacterState.MOVE);
        AddTargetState(() => _isInMinAgro, E_CharacterState.DETECTED);
    }
}
