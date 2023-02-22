using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowBackState : EnemyBaseBackState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowBackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isBackOver,E_CharacterState.DETECTED);
    }   
}
