using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullLandState : EnemyBaseLandState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    public EnemySkullLandState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.DETECTED);
    }
}
