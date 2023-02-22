using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWarriorInAirState : EnemyBaseInAirState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    public EnemyWarriorInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDead, E_CharacterState.DEAD);
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.DETECTED);
        //如果在空中超过一段时间
    }
}
