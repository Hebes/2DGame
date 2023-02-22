using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseDetectedState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isInMinAgro;//在严重警惕范围中
    protected bool _isInMaxAgro;//在警惕范围中
    protected bool _isInMeleeAttack;//在近战攻击范围内
    protected bool _isInRangeAttack;//在远程攻击范围内
    protected bool _inOverMaxWaitTime;
    protected bool _isWallFront;
    protected bool _isLedgeVertical;
    private float _lastFlipTime = float.NegativeInfinity;
    public EnemyBaseDetectedState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Move.SetXVelocity(0);
        _inOverMaxWaitTime = false;
        _lastFlipTime = 0;
    }
    public override void Check()
    {
        base.Check();
        _isInMinAgro = ColliderCheck.PlayerInMinArgo;
        _isInMaxAgro = ColliderCheck.PlayerInMaxArgo;
        _isInMeleeAttack = ColliderCheck.PlayerInMeleeAttackDis;
        _isInRangeAttack = ColliderCheck.PlayerInRangeAttackDis;
        _isWallFront = ColliderCheck.WallFront;
        _isLedgeVertical = ColliderCheck.LedgeVerticalFront;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocity(0);
        CheckIsOverMaxWaitTime();
        if(Time.time>=_lastFlipTime+_data.detectedMinFlipTime)
        {
            Move.LookAtPlayer();
            _lastFlipTime = Time.time;
        }
    }
    //检测是否到达最大等待时间
    public void CheckIsOverMaxWaitTime()
    {
        if (!_inOverMaxWaitTime && Time.time >= _enterTime + _data.maxDetectedTime)
            _inOverMaxWaitTime = true;
    }
}
