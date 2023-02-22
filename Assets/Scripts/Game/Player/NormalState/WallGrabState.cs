using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrabState : TouchingBaseState
{
    protected Vector3 _holdPos;
    public WallGrabState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        //应该是优先进入滑墙状态
        AddTargetState(() => _isWallFront && !_ledgeHorizontal && !_isGround, E_CharacterState.LEDGECLIMB);
        AddTargetState(() => (_yInput == -1)||(!_grabInput&&_xInput==Move.FacingDir), E_CharacterState.WALLSLIDE);
        AddTargetState(() => _yInput == 1&&_ledgeHorizontal,E_CharacterState.WALLCLIMB);
    }
    public override void Enter()
    {
        base.Enter();
        //但是只是清零不会固定位置不变的 因为他还会受到重力的影响
        _holdPos = _ower.transform.position;
        Move.SetVelocityZero();
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetVelocityZero();
        _ower.SetPosition(_holdPos);
    }
}
