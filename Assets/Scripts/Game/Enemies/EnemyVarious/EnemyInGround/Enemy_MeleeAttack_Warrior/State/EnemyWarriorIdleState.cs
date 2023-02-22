using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWarriorIdleState : EnemyBaseIdleState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    public EnemyWarriorIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isGrounded, E_CharacterState.INAIR);
        AddTargetState(() => _isInMinAgro || _playerInBack, E_CharacterState.DETECTED);
    }
}
