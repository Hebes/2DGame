/****************************************************
    文件：BossMinotaurReadyState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 12:38:58
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurReadyState : BossBaseReadyState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private string _audioClipName = "enemy_minotaur_ready";
    public BossMinotaurReadyState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.MELEEATTACKTWO);
    }
    public override void Enter()
    {
        base.Enter();
        Behavior.SetDomineering(true);
        AudioMgr.Instance.PlayOnce(_audioClipName);

    }
    public override void Exit()
    {
        base.Exit();
        Behavior.SetDomineering(false);
    }
}