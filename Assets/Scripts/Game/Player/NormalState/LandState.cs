using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : GroundBaseState
{
    private string _audioClipName = "hero_land_soft";
    public LandState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_xInput!=0,E_CharacterState.MOVE);
        AddTargetState(()=>AnimFinish,E_CharacterState.IDLE);
    }
    public override void Enter()
    {
        base.Enter();
        Move.SetVelocityZero();
        _ower.CreateLandEffect();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
