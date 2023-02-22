using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TouchingBaseState :HeroManBaseState
{

    //检车相关
    protected bool _isWallFront;
    protected bool _isWallBack;
    protected bool _ledgeHorizontal;
    protected bool _isGround;


    //输入相关
    protected int _xInput;
    protected int _yInput;
    protected bool _jumpInput;
    protected bool _grabInput;



    public TouchingBaseState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_grabInput&&_isGround && Move.GetCurVelocity.y <= 0.01f, E_CharacterState.IDLE);
        AddTargetState(() => _isWallFront/* && _xInput ==-Move.FacingDir */&& _jumpInput , E_CharacterState.WALLJUMP);
        //满足 不触碰墙直接进入空气状态 
        //或者 没有爬墙命令并且x轴上的移动方向和面朝向不一样
        AddTargetState(()=>!_isWallFront||(_xInput!=Move.FacingDir&&!_grabInput),E_CharacterState.INAIR,()=>_fsm.GetState<InAirState>().StartWallCraceTime());
    }
    public override void Enter()
    {
        base.Enter();
        _fsm.GetState<DashState>().SetReadyDash(true);
    }
    public override void Check()
    {
        base.Check();
        _isWallFront = ColliderCheck.WallFront;
        _isWallBack = ColliderCheck.WallBack;
        _ledgeHorizontal = ColliderCheck.LedgeHorizontal;
        _isGround = ColliderCheck.Ground;
        if (_isWallFront && !_ledgeHorizontal)
            _fsm.GetState<LedgeClimbState>().SetDetectedPos(_ower.transform.position);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _xInput = _ower.InputHandle.XInput;
        _yInput = _ower.InputHandle.YInput;
        _jumpInput = _ower.InputHandle.JumpInput;
        _grabInput = _ower.InputHandle.GrabInput;
    }
}
