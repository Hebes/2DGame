using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : GroundBaseState
{
    private string _audioClipName = "hero_land_hard";
    public RollState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => (AnimFinish || _isWallFront) && _isCeiling, E_CharacterState.CROUCHIDLE);
        AddTargetState(() => AnimFinish || _isWallFront, E_CharacterState.IDLE);
    }
    public override void Enter()
    {
        base.Enter();
        Move.SetXVelocity(Move.FacingDir * _data.rollSpeed);
        _ower.ChangeHeroColliderHeight(_data.crouchHeight);
        _fsm.GetState<JumpState>().SetCanJump(false);
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void Exit()
    {
        base.Exit();
        _ower.ChangeHeroColliderHeight(_data.standHeight);
        _fsm.GetState<JumpState>().SetCanJump(true);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocity(Move.FacingDir * _data.rollSpeed);
    }
}
