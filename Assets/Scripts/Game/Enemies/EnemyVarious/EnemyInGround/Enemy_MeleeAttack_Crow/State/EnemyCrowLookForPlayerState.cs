using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowLookForPlayerState : EnemyBaseLookForPlayerState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowLookForPlayerState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isInMaxArgo, E_CharacterState.DETECTED);
        AddTargetState(() => _isLookForOver, E_CharacterState.MOVE);
    }
}
