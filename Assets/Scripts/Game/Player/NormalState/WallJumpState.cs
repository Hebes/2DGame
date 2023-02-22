using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : AbilityBaseState
{
    private string _audioClipName = "hero_jump";
    public WallJumpState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        //因为考虑到n段跳的缘故
        _fsm.GetState<JumpState>().ResetJumpCountNum();
        _fsm.GetState<JumpState>().ReduceJumpCountNum();
        Move.SetVelocityInDir(_data.wallJumpStrength,_data.wallJumpDir,false);
        Move.Flip();//自动翻转
        _ower.InputHandle.SetJumpInput();
        _ower.CreateJumpEffect();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _anim.SetFloat(Consts.CHARACTER_ANM_XVleocity,Move.GetCurVelocity.x);
        _anim.SetFloat(Consts.CHARACTER_ANM_YVleocity,Move.GetCurVelocity.y);
        CheckWallJumpTimeOver();
    }
    //检测是否可以墙跳
    public void CheckWallJumpTimeOver()
    {
        if (Time.time >= _enterTime + _data.wallJumpTime)
            SetAbilityDown();
    }
    public override E_CharacterState TargetState() => E_CharacterState.INAIR;
}
