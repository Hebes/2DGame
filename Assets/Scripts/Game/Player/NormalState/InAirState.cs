using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : HeroManBaseState
{
    protected float _offsetTime;
    protected float _lastCraceTime;//上一次开启土狼时间的时间
    protected bool _openCraceTime;//是否开启土狼时间


    protected bool _openWallCraceTime;//是否开启墙跳土狼时间
    protected float _lastOpenWallCraceTime;

    //输入相关
    protected bool _isJumpingUp;//是否有向上的加速度
    protected bool _jumpInput;//跳跃命令输入
    protected bool _jumpInputStop;
    protected int _xInput;
    protected bool _grabInput;
    protected bool _dashInput;
    protected bool _attackInput;
    protected bool _bowInput;
    protected int _yInput;

    //检测相关
    protected bool _isOnGround;
    protected bool _isWallFront;
    protected bool _isWallBack;
    protected bool _isLedgeHorizontal;


    private float _roolTimer;
    protected bool _enterRollState;//是否进入翻滚状态

    private string _audioClipName = "hero_falling";
    public InAirState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _bowInput, E_CharacterState.RANGEAIRATTACK);
        AddTargetState(() => _attackInput&&_yInput==-1, E_CharacterState.MELEEAIRATTACK_LOOP);
        AddTargetState(()=>_attackInput,E_CharacterState.MELEEAIRATTACK);
        AddTargetState(() => _isOnGround && Move.GetCurVelocity.y < 0.01f&&_enterRollState, E_CharacterState.ROLL);
        AddTargetState(()=>_isOnGround&&Move.GetCurVelocity.y<0.01f,E_CharacterState.LAND);
        AddTargetState(()=> (_isWallFront  || _openWallCraceTime) && _xInput != Move.FacingDir && _jumpInput, E_CharacterState.WALLJUMP);
        AddTargetState(()=>_dashInput&&!_isWallFront&&_fsm.GetState<DashState>().CheckCanDash(),E_CharacterState.DASH);
        //进入爬墙状态
        AddTargetState(()=> _isWallFront && !_isLedgeHorizontal && !_isOnGround ,E_CharacterState.LEDGECLIMB);
        //进入攀爬状态      
        AddTargetState(() => _isWallFront&&_grabInput, E_CharacterState.WALLGRAB);
        //进入滑墙状态
        AddTargetState(()=> _isWallFront && Move.GetCurVelocity.y < 0.01f && _xInput == Move.FacingDir, E_CharacterState.WALLSLIDE);
        AddTargetState(()=> _jumpInput && _openCraceTime && _fsm.GetState<JumpState>().CheckIfCanJump(),E_CharacterState.JUMP);
    }
    public override void Check()
    {
        base.Check();
        _isOnGround = ColliderCheck.Ground;
        _isWallFront = ColliderCheck.WallFront;
        _isWallBack = ColliderCheck.WallBack;
        _isLedgeHorizontal = ColliderCheck.LedgeHorizontal;
        if (_isWallFront && !_isLedgeHorizontal)
            _fsm.GetState<LedgeClimbState>().SetDetectedPos(_ower.transform.position);
    }
    public override void Enter()
    {
        base.Enter();
        _enterRollState = false;
        _roolTimer = 0f;
    }
    public override void Exit()
    {
        base.Exit();
        _isJumpingUp = false;
        AudioMgr.Instance.PauseNow(_audioClipName);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _xInput = _ower.InputHandle.XInput;
        _yInput = _ower.InputHandle.YInput;
        _grabInput = _ower.InputHandle.GrabInput;
        _jumpInput = _ower.InputHandle.JumpInput;
        _dashInput = _ower.InputHandle.DashInput;
        _attackInput = _ower.InputHandle.AttackInput;
        _bowInput = _ower.InputHandle.BowInput;
        Move.CheckCanFlip(_xInput);
        Move.SetXVelocity(_data.moveVelocity * _xInput);
        SetAnimationArgs();
        CheckCraceTime();
        CheckWallCraceTime();
        CheckJumpUp();
        CheckJumpInput();
        CheckEnterIfRollState();
    }
   
    private void SetAnimationArgs()
    {
        _anim.SetFloat(Consts.CHARACTER_ANM_XVleocity, Mathf.Abs(Move.GetCurVelocity.x));
        _anim.SetFloat(Consts.CHARACTER_ANM_YVleocity, Move.GetCurVelocity.y);
    }
    //跳跃检测
    private void CheckJumpInput()
    {
        _jumpInput = _ower.InputHandle.JumpInput;      
        _jumpInputStop = _ower.InputHandle.JumpInputStop;
        //如果正在向上飞行 松开了跳跃按钮
        if (_isJumpingUp && _jumpInputStop)
        {
            _offsetTime = Time.time - _ower.InputHandle._lastJumpDownTime;
            for (int i = _data.jumpHeightMsgs.Length-1; i >= 0; i--)
            {
                if (_offsetTime >= _data.jumpHeightMsgs[i]._offsetTime)
                {
                    var msg = _data.jumpHeightMsgs[i];
                    var move = _core.Get<RgMoveComponent>();
                    var curYVelocity = move.GetCurVelocity.y;
                    move.SetYVelocity(curYVelocity * msg._multiple);
                    break;
                }
            }
            //todo 这里可能是后患
            _ower.InputHandle.SetJumpInputStop(false);
        }
    }
    private void CheckJumpUp()
    {
        if (_isJumpingUp && _core.Get<RgMoveComponent>().GetCurVelocity.y < 0.01f)
            _isJumpingUp = false;
    }
    public void SetJumpingUpTrue() => _isJumpingUp = true;

    //开启土狼时间
    public void StartCraceTime()
    {
        if(!_openCraceTime)
        {
            _openCraceTime = true;
            _lastCraceTime = Time.time;
        }       
    }
    public void StartWallCraceTime()
    {
        if(!_openWallCraceTime)
        {
            _openWallCraceTime = true;
            _lastOpenWallCraceTime = Time.time;
        }
    }

    //检测土狼时间
    public void CheckCraceTime()
    {
        if (_openCraceTime && Time.time >= _lastCraceTime + _data.craceTime)
            _openCraceTime = false;
    }
    public void CheckWallCraceTime()
    {
        if (_openWallCraceTime && Time.time >= _lastOpenWallCraceTime + _data.wallCraceTime)
            _openWallCraceTime = false;
    }
    public void CheckEnterIfRollState()
    {
        if(!_enterRollState&&Move.GetCurVelocity.y<0)
        {
            _roolTimer += Time.deltaTime;
            if (_roolTimer >= _data.roolMinStartTime)
                _enterRollState = true;
        }
    }
}

