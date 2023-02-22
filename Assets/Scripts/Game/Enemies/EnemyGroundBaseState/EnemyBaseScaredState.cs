using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScaredState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isWallFront;
    protected bool _isLedgeVerticalFront;
    protected bool _isScaredStateOver;
    public EnemyBaseScaredState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isScaredStateOver = false;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        if (_isWallFront||!_isLedgeVerticalFront)
            Move.Flip();
        Move.SetXVelocityInFacing(_data.scaredVelocity,true);
        CheckScaredIsOver();
    }
    public override void Check()
    {
        base.Check();
        _isWallFront = ColliderCheck.WallFront;
        _isLedgeVerticalFront = ColliderCheck.LedgeVerticalFront;
    }
    //检测受惊是否结束
    public void CheckScaredIsOver()
    {
        if (!_isScaredStateOver && Time.time >= _enterTime + _data.scaredTime)
            _isScaredStateOver = true;
    }
}
