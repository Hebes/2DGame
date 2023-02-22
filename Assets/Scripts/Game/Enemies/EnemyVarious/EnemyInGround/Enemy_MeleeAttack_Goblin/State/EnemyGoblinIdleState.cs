using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGoblinIdleState : EnemyBaseIdleState<Enemy_MeleeAttack_Goblin, EnemyGoblineData>
{
    public EnemyGoblinIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Goblin ower, EnemyGoblineData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(()=>_isIdleOver,E_CharacterState.MOVE);
    }
}
