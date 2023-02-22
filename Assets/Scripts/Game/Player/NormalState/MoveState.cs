using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundBaseState
{
    private string _audioClipName = "hero_run_footsteps_stone";
    protected bool _enterSlide;
    protected bool _dashInput;
    protected bool _bowInput;
    public MoveState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_bowInput,E_CharacterState.RANGEATTACK);
        AddTargetState(() => _attackInput , E_CharacterState.MELEEATTACK);
        AddTargetState(() => _dashInput && _fsm.GetState<DashState>().CheckCanDash(), E_CharacterState.DASH);
        AddTargetState(() => _ledgeHorizonal && _isWallFront && _grabInput, E_CharacterState.WALLGRAB);
        AddTargetState(() => _yInput == -1&&_enterSlide, E_CharacterState.SLIDE);
        AddTargetState(() => _yInput == -1, E_CharacterState.CROUCHIDLE);
        AddTargetState(()=>_xInput == 0,E_CharacterState.IDLE);
        AudioMgr.Instance.Play(_audioClipName,true);
        AudioMgr.Instance.PauseNow(_audioClipName);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _dashInput = _ower.InputHandle.DashInput;
        _bowInput = _ower.InputHandle.BowInput;
        CheckEnterSlideState();
        Move.CheckCanFlip(_xInput);
        Move.SetXVelocity(_data.moveVelocity*_xInput);
    }
    public override void Enter()
    {
        base.Enter();
        _enterSlide = false;
        GameStateModel.Instance.CanInteractive = true;
        _ower.SetWalkEffectActive(true);
        AudioMgr.Instance.Replay(_audioClipName);
    }
    public override void Exit()
    {
        base.Exit();
        GameStateModel.Instance.CanInteractive = false;
        _ower.SetWalkEffectActive(false);
        AudioMgr.Instance.PauseNow(_audioClipName);
    }
    public void CheckEnterSlideState()
    {
        if (Time.time >= _enterTime + _data._slideMinStartTime)
            _enterSlide = true;
    }
}
