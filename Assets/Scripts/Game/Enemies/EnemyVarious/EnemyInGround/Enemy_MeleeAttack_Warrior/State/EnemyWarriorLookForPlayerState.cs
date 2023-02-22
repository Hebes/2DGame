using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWarriorLookForPlayerState : EnemyBaseLookForPlayerState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    public EnemyWarriorLookForPlayerState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isInMaxArgo, E_CharacterState.DETECTED);
        AddTargetState(() => _isLookForOver, E_CharacterState.IDLE);
    }
}