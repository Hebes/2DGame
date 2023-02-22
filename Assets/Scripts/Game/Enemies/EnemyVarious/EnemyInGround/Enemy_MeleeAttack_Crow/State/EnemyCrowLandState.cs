using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowLandState : EnemyBaseLandState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowLandState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => AnimFinish, E_CharacterState.BACK);
    }    
}
