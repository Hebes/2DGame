using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullHitState : EnemyBaseHitState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    public EnemySkullHitState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish&&!_isGround,E_CharacterState.INAIR);
        AddTargetState(()=>AnimFinish,E_CharacterState.BACK);
    }
}
