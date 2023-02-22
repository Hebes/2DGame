using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseLookForPlayerState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isWallFront;
    protected bool _isLedgeVertical;
    protected bool _isInMaxArgo;//在警惕范围中
    protected bool _isLookForOver;//是否寻找完毕
    protected int _curTurnCount;
    protected float _lastTurnTime;//上一次转向的时间
    protected bool _isTurning;//是否正在转向中
    public EnemyBaseLookForPlayerState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isLookForOver = false;
        _isTurning = false;
        _curTurnCount = 0;
    }
    public override void Check()
    {
        base.Check();
        _isWallFront = ColliderCheck.WallFront;
        _isLedgeVertical = ColliderCheck.LedgeVerticalFront;
        _isInMaxArgo = ColliderCheck.PlayerInMaxArgo;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckIsTurnOver();
    }
    //检测是否完成进行巡逻转向的逻辑
    protected void CheckIsTurnOver()
    {
        if (_isLookForOver)
            return;
        Move.SetXVelocityInFacing(_data.lookForPlayerVelocity,true);
        if (_curTurnCount == _data.turnNumber)
        {
            _isLookForOver = true;
            return;
        }
        if (_isWallFront || !_isLedgeVertical)
            Move.SetVelocityZero();
        if (_isTurning)
        {          
            if (Time.time >= _lastTurnTime + _data.turnTime)
                _isTurning = false;
        }
        else
        {
            Move.Flip();
            _isTurning = true;
            _curTurnCount++;
            _lastTurnTime = Time.time;
        }
    }
}
