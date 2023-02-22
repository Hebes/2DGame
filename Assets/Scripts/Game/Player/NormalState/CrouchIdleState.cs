using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchIdleState : GroundBaseState
{
    public CrouchIdleState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _yInput != -1 && !_isCeiling, E_CharacterState.IDLE);
        AddTargetState(()=>_xInput!=0,E_CharacterState.CROUCHMOVE);
    }
    public override void Check()
    {
        base.Check();
    }
    public override void Enter()
    {
        base.Enter();
        Move.SetVelocityZero();
        _ower.ChangeHeroColliderHeight(_data.crouchHeight);
    }
    public override void Exit()
    {
        base.Exit();
        _ower.ChangeHeroColliderHeight(_data.standHeight);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetVelocityZero();
    }
}
