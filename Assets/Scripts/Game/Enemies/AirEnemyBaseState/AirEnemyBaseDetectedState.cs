using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseDetectedState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isDetectedOver;
    protected bool _isPlayerInMaxArgo;
    protected bool _isPlayerInMinArgo;
    protected bool _isPlayerInRangeAttack;
    protected float _lastFlipTime=float.NegativeInfinity;
    protected bool _canFlip;
    protected Vector3 _enterPos;//进来的时的位置
    public AirEnemyBaseDetectedState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isDetectedOver = false;
        Move.SetVelocityZero();
        _canFlip = true;
        _enterPos = _ower.transform.position;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetVelocityZero();
        _ower.transform.position = _enterPos;
        LookAtPlayer();
        CheckDetectedIsOver();
        CheckCanFlip();
        //固定住位置
    }
    public override void Check()
    {
        base.Check();
        _isPlayerInMaxArgo = ColliderCheck.PlayerInMaxArgo;
        _isPlayerInMinArgo = ColliderCheck.PlayerInMinArgo;
        _isPlayerInRangeAttack = ColliderCheck.PlayerInRangeAttackDis;
    }
    //检测是否观察结束
    private void CheckDetectedIsOver()
    {
        if (!_isDetectedOver && Time.time >= _enterTime + _data.maxDetectedTime)
            _isDetectedOver = true;
    }
    protected void LookAtPlayer()
    {
        if (_canFlip)
        {
            Move.LookAtPlayer();
            _lastFlipTime = Time.time;
            _canFlip = false;
        }
    }
    //检测是否能转向
    private void CheckCanFlip()
    {
        if (!_canFlip && Time.time >= _lastFlipTime + _data.detectedMinFlipTime)
            _canFlip = true;
    }
}

