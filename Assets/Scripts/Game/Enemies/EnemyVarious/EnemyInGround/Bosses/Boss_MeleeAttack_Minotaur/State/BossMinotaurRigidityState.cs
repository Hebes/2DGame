/****************************************************
    文件：BossMinotaurRigidityState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 12:38:40
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurRigidityState : BossBaseRigidityState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private string _audioClipName = "enemy_minotaur_rigidity";
    public BossMinotaurRigidityState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}