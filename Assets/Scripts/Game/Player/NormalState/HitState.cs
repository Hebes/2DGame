using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : AbilityBaseState
{
    protected bool _isGround;
    protected bool _isWallFront;
    protected bool _isHitOverResetPos;//是否受伤动画播放完毕 重置主角位置

    private string _animClipName = "hero_damage";
    private string _animClipNameLess = "hero_damage_less_harsh";
    public HitState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Move.SetXVelocity(0);
        SetHitVelocity();
        AudioMgr.Instance.PlayOnce(_isHitOverResetPos ? _animClipName : _animClipNameLess);
    }
    private void SetHitVelocity()
    {
        if (_isHitOverResetPos)
        {
            Move.SetVelocityZero();
            return;
        }
        if (!_isWallFront)
        {
            //如果不在地上
            if (!_isGround)
                Move.SetVelocity(Behavior._strength, Behavior._hitDir);
            //如果在地上水平方向上的力向上
            else
                Move.SetVelocityButAbsY(Behavior._strength, Behavior._hitDir);
        }
        else
            Move.SetVelocityButAbsY(Behavior._strength, Vector2.one * -Move.FacingDir);
    }
    public override void Exit()
    {
        base.Exit();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_HITOVER);
        // todo 添加主角重置位置的功能
        if (_isHitOverResetPos)
            _ower.ResetPos();
        _isHitOverResetPos = false;
    }
    public override void Check()
    {
        base.Check();
        _isGround = ColliderCheck.Ground;
        _isWallFront = ColliderCheck.WallFront;
    }
    public override E_CharacterState TargetState()
        => E_CharacterState.INAIR;
    //设置是否受伤动画播放完毕后 重置位置
    public void SetIsResetPos()
        => _isHitOverResetPos = true;
    public override void AnimatorFinishTrigger()
    {
        base.AnimatorFinishTrigger();
        SetAbilityDown();
    }
}
