using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseMoveState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isInMinAgro;//是否在严重警惕的范围内
    protected bool _playerInBack;//主角是否在背后被发现


    protected bool _isLedgeVerticalFront;
    protected bool _isWallFront;
    protected bool _isWallBack;
    protected bool _isGrounded;

    public EnemyBaseMoveState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();        
        Move.SetXVelocityInFacing(_data.moveVelocity,true);
    }
    public override void Check()
    {
        base.Check();
        _isWallFront = ColliderCheck.WallFront;
        _isWallBack = ColliderCheck.WallBack;
        _isLedgeVerticalFront = ColliderCheck.LedgeVerticalFront;
        _isInMinAgro = ColliderCheck.PlayerInMinArgo;
        _playerInBack = ColliderCheck.PlayerInBack;
        _isGrounded = ColliderCheck.Ground;
    }
}
