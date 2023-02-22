using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseHitState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isWallFront;//是不是面对墙
    protected bool _isWallBack;//是不是背对墙
    protected bool _isRangeLedgeVerticalFront;
    protected bool _isRangeLedgeVerticalBack;
    protected bool _isGround;
    protected bool _isLedgeVerticalFront;
    protected bool _isLedgeVerticalBack;

    public EnemyBaseHitState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        SetHitVelocity();
    }
    //设置受伤受到的力
    private void SetHitVelocity()
    {
        if (!_isWallFront)
        {
            //如果不在地上
            if (!_isGround)
                Move.SetVelocity(Behavior._strength, Behavior._hitDir);
            //如果在地上水平方向上的力向上
            else
                Move.SetVelocityButAbsY(Behavior._strength, (Behavior._enemyHitDir * Behavior._hitDir).normalized);
        }
        else
            Move.SetVelocityButAbsY(Behavior._strength, Vector2.one * -Move.FacingDir);
    }
    public override void Exit()
    {
        base.Exit();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_HITOVER);
    }
    public override void Check()
    {
        base.Check();
        _isWallFront = ColliderCheck.WallFront;
        _isWallBack = ColliderCheck.WallBack;
        _isRangeLedgeVerticalBack = ColliderCheck.RangeLedgeVerticalBack;
        _isRangeLedgeVerticalFront = ColliderCheck.RangeLedgeVerticalFront;
        _isGround = ColliderCheck.Ground;
        _isLedgeVerticalBack = ColliderCheck.LedgeVerticalBack;
        _isLedgeVerticalFront = ColliderCheck.LedgeVerticalFront;
    }
}
