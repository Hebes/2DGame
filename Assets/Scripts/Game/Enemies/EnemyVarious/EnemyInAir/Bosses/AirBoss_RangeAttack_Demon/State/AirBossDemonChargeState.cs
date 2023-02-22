/****************************************************
    文件：AirBossDemonChargeState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 16:44:49
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossDemonChargeState : AirBossBaseChargeState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private string _audioClipName = "enemy_demon_charge";
    public AirBossDemonChargeState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => PlayerTf == null, E_CharacterState.IDLE);
        AddTargetState(() => _isInRangeAttackDis, E_CharacterState.DETECTED);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}