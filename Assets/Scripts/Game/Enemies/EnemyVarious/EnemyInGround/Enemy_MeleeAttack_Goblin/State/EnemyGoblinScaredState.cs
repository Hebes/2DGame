using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinScaredState : EnemyBaseScaredState<Enemy_MeleeAttack_Goblin, EnemyGoblineData>
{
    public EnemyGoblinScaredState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Goblin ower, EnemyGoblineData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isScaredStateOver, E_CharacterState.MOVE);
    }
}
