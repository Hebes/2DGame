using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressLookForPlayerState : EnemyBaseLookForPlayerState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    public EnemyHuntressLookForPlayerState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isInMaxArgo, E_CharacterState.DETECTED);
        AddTargetState(() => _isLookForOver, E_CharacterState.MOVE);
    }
}
