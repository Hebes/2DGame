using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHuntressHitState : EnemyBaseHitState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    public EnemyHuntressHitState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => AnimFinish && !_isGround, E_CharacterState.INAIR);
        AddTargetState(() => AnimFinish, E_CharacterState.BACK);
    }
}
