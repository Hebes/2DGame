using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowDetectedState : EnemyBaseDetectedState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    public EnemyCrowDetectedState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isInMeleeAttack,E_CharacterState.MELEEATTACK);
        //如果主角丢失在最大视野范围    则寻找主角
        AddTargetState(()=>_inOverMaxWaitTime&&!_isInMaxAgro,E_CharacterState.LOOKFOR);
        //不能盲目追逐主角
        AddTargetState(()=>_inOverMaxWaitTime && !_isWallFront && _isLedgeVertical, E_CharacterState.CHARGE);
    }
}
