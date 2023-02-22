using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowIdleState : EnemyBaseIdleState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>!_isGrounded,E_CharacterState.INAIR);
        AddTargetState(ChangeCanIntoDetectedState, E_CharacterState.DETECTED );
        AddTargetState(() => _isIdleOver, E_CharacterState.MOVE);
    }   
    private bool ChangeCanIntoDetectedState()
    {
        if (_playerInBack)
            return true;
        if (_isInMinAgro)
            return true;
        return false;
    }

}
