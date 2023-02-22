using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundBaseState :HeroManBaseState
{
    protected int _xInput;
    protected int _yInput;
    protected bool _jumpInput;
    protected bool _grabInput;
    protected bool _attackInput;

    protected bool _isOnGround;
    protected bool _isCeiling;
    protected bool _isWallFront;
    protected bool _ledgeHorizonal;

    public GroundBaseState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        //进入空中状态     会自动开启土狼时间
        AddTargetState(()=>!_isOnGround,E_CharacterState.INAIR,()=> _fsm.GetState<InAirState>().StartCraceTime());
        AddTargetState(()=>!_isCeiling&&_jumpInput&& _fsm.GetState<JumpState>().CheckIfCanJump(), E_CharacterState.JUMP);
    }
    public override void Check()
    {
        base.Check();
        _isOnGround = ColliderCheck.Ground;
        _isCeiling = ColliderCheck.Ceiling;
        _isWallFront = ColliderCheck.WallFront;
        _ledgeHorizonal = ColliderCheck.LedgeHorizontal;
    }
    public override void Enter()
    {
        base.Enter();
        _fsm.GetState<JumpState>().ResetJumpCountNum();
        _fsm.GetState<DashState>().SetReadyDash(true);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _xInput = _ower.InputHandle.XInput;
        _yInput = _ower.InputHandle.YInput;
        _jumpInput = _ower.InputHandle.JumpInput;
        _grabInput = _ower.InputHandle.GrabInput;
        _attackInput = _ower.InputHandle.AttackInput;
    }
}
