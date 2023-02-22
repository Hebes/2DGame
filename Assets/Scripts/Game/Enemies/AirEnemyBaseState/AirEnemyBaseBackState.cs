using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseBackState<T, X> : AirEnemyAbilityState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    protected Vector2 _backDir;
    protected bool _isGround;
    protected bool _isBackOver;
    protected bool _isPlayerInMinArgo;
    public AirEnemyBaseBackState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isBackOver = false;
        ResetBackDir();
    }
    public override void Exit()
    {
        base.Exit();
        Move.SetVelocityZero();
    }
    public override void Check()
    {
        base.Check();
        _isPlayerInMinArgo = ColliderCheck.PlayerInMinArgo;
        _isAbilityDown = ColliderCheck.Ground;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.LookAtPlayer();
        CheckIsBackOver();
        if (!_isBackOver)
            Move.SetVelocityButAbsY(_data.backMovementVelocity, _backDir);
    }
    //重置方向
    protected void ResetBackDir()
    {
        if (PlayerTf == null)
            return;
        _backDir = (_ower.transform.position - Move.PlayerTf.position) * _data.backDirMult;
        _backDir.Normalize();
    }
    private void CheckIsBackOver()
    {
        if (!_isBackOver &&
            (Time.time >= _enterTime + _data.backTime||(_isGround/*&&Time.time>_enterTime+_data.minBackTime*/)||!_isPlayerInMinArgo))
            _isBackOver = true;
    }
}
