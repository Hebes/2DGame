using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWormDetectedState : EnemyBaseDetectedState<Enemy_RangeAttack_FireWorm, EnemyFireWormData>
{
    public EnemyFireWormDetectedState(FiniteStateMachine fsm, string animBoolName, Enemy_RangeAttack_FireWorm ower, EnemyFireWormData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_inOverMaxWaitTime&&_isInRangeAttack&&fsm.GetState<EnemyFireWormRangeAttackState>().CheckCanEnterRangeAttack(),E_CharacterState.RANGEATTACK);
        AddTargetState(()=> _inOverMaxWaitTime&&!_isInMaxAgro,E_CharacterState.MOVE);
    }
}
