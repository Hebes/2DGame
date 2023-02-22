using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirBossDemonIdleState : AirBossBaseIdleState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    public AirBossDemonIdleState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {


    }
    public override void Enter()
    {
        base.Enter();
        _curDir = Vector2.right;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        IdleFlyLeftAndRight();
    }
}
