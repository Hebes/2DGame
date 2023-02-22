using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : GroundBaseState
{
    public SlideState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        //优先进入蹲下状态
        AddTargetState(()=>_jumpInput,E_CharacterState.JUMP);
        AddTargetState(() => (AnimFinish || _isWallFront) && (_yInput == -1||_isCeiling), E_CharacterState.CROUCHIDLE);
        AddTargetState(()=>AnimFinish||_isWallFront,E_CharacterState.IDLE);
    }
    public override void Enter()
    {
        base.Enter();
        Move.SetXVelocity(Move.FacingDir*_data.slideSpeed);
        _ower.ChangeHeroColliderHeight(_data.crouchHeight);
        _fsm.GetState<JumpState>().SetCanJump(false);
        _ower.SetWalkEffectActive(true);
    }
    public override void Exit()
    {
        base.Exit();
        _ower.ChangeHeroColliderHeight(_data.standHeight);
        _fsm.GetState<JumpState>().SetCanJump(true);
        _ower.SetWalkEffectActive(false);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocity(Move.FacingDir * _data.slideSpeed);
    }
}
