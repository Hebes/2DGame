/****************************************************
    文件：GetItemState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/24 12:59:51
	功能：主角拾取物品状态
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GetItemState : AbilityBaseState
{
    public GetItemState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {


    }
    public override void Enter()
    {
        base.Enter();
        Move.SetXVelocity(0);
    }
    public override void AnimatorFinishTrigger()
    {
        base.AnimatorFinishTrigger();
        SetAbilityDown();
    }
    public override E_CharacterState TargetState()
        => E_CharacterState.IDLE;
}