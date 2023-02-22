using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWormHitState : EnemyBaseHitState<Enemy_RangeAttack_FireWorm, EnemyFireWormData>
{
    public EnemyFireWormHitState(FiniteStateMachine fsm, string animBoolName, Enemy_RangeAttack_FireWorm ower, EnemyFireWormData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>!_isGround,E_CharacterState.INAIR);
        //todo 此处可能需要立马修改
        AddTargetState(()=>AnimFinish,E_CharacterState.RANGEATTACK);
    }
}
