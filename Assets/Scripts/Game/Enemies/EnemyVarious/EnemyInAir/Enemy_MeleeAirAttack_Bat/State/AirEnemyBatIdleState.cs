using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBatIdleState : AirEnemyBaseIdleState<Enemy_MeleeAirAttack_Bat, AirEnemyBatData>
{
    private int _curChangeXNum;//当前满足改变x方向的条件
    public AirEnemyBatIdleState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAirAttack_Bat ower, AirEnemyBatData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _curDir = Vector2.one;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocity(_data.idleStartXVelocity*Mathf.Sign(_curDir.x));
        IdleFlyUpAndDown();
    }
    protected override void ReverserCurY()
    {
        base.ReverserCurY();
        if (++_curChangeXNum > _data.idleChangeXDirNum)
        {
            ReverserCurX();
            _curChangeXNum = 0;
        }
    }
}
