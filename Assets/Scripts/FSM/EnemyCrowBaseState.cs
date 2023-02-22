using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyCrowBaseState : EnemyBaseState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowBaseState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {


    }
}
  
