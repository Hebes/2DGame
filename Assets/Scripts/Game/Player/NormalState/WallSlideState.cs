using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : TouchingBaseState
{
    private string _audioClipName = "hero_wall_slide";
    public WallSlideState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _grabInput&&_yInput!=-1, E_CharacterState.WALLGRAB);
        AudioMgr.Instance.Play(_audioClipName,true);
        AudioMgr.Instance.PauseNow(_audioClipName);
    }
    public override void Enter()
    {
        base.Enter();
        _ower.SetWalkEffectActive(true);
        AudioMgr.Instance.Replay(_audioClipName);
    }
    public override void Exit()
    {
        base.Exit();
        _ower.SetWalkEffectActive(false);
        AudioMgr.Instance.PauseNow(_audioClipName);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetYVelocity(-_data.wallSlideVelocity);
    }
}
