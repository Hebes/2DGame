using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeJumpState : AbilityBaseState
{
    private string _audioClipName = "hero_climb";
    public LedgeJumpState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override E_CharacterState TargetState() => E_CharacterState.INAIR;
    public override void AnimatorFinishTrigger()
    {
        base.AnimatorFinishTrigger();
        //动画播放完毕之后才能切换状态
        SetAbilityDown();
    }
}
