using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundBaseState
{
    protected bool _dashInput;
    protected bool _bowInput;
    public IdleState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _bowInput, E_CharacterState.RANGEATTACK);
        AddTargetState(() => _attackInput, E_CharacterState.MELEEATTACK);
        AddTargetState(() => _dashInput && _fsm.GetState<DashState>().CheckCanDash(), E_CharacterState.DASH);
        AddTargetState(() => _ledgeHorizonal && _isWallFront && _grabInput, E_CharacterState.WALLGRAB);
        AddTargetState(() => _yInput == -1, E_CharacterState.CROUCHIDLE);
        AddTargetState(() => _xInput != 0 && !_isCeiling, E_CharacterState.MOVE);
    }
    public override void Enter()
    {
        base.Enter();
        Move.SetVelocityZero();
        GameStateModel.Instance.CanInteractive = true;
    }
    public override void Exit()
    {
        base.Exit();
        GameStateModel.Instance.CanInteractive = false;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetVelocityZero();
        _dashInput = _ower.InputHandle.DashInput;
        _bowInput = _ower.InputHandle.BowInput;
    }
}
