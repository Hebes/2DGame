/****************************************************
    文件：AirBossDemonDashTwoState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 16:3:49
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossDemonMeleeAttackTwoState : AirBossBaseMeleeAttackState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private string _audioClipName = "enemy_demon_meleeAttack";
    public AirBossDemonMeleeAttackTwoState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => !_isAbilityDown, E_CharacterState.DISAPPEAR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.LookAtPlayer();
        Move.SetXVelocityInFacing(_data.meleeAttackTwoVelocity,true);
    }
}