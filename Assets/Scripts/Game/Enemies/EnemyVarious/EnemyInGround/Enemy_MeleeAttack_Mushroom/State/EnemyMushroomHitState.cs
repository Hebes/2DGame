using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomHitState : EnemyBaseHitState<Enemy_MeleeAttack_Mushroom, EnemyMushroomData>
{
    public EnemyMushroomHitState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Mushroom ower, EnemyMushroomData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.MOVE);
    }
  
}
