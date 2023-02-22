using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbState : TouchingBaseState
{
    public WallClimbState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isWallFront && !_ledgeHorizontal && !_isGround, E_CharacterState.LEDGECLIMB);
        AddTargetState(() => (_yInput == -1) || (!_grabInput && _xInput == Move.FacingDir), E_CharacterState.WALLSLIDE);
        AddTargetState(() => _yInput == 0|| !_ledgeHorizontal, E_CharacterState.WALLGRAB);
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Check()
    {
        base.Check();      
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetYVelocity(_data.wallClimbVelocity);
    }
}
