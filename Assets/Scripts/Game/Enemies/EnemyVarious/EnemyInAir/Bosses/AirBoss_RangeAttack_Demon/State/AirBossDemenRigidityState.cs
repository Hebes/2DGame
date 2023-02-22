/****************************************************
    文件：AirBossDemenRigidityState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/23 19:38:47
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossDemenRigidityState : AirBossBaseRigidityState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private string _audioClipName = "enemy_demon_rigidity";
    public AirBossDemenRigidityState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isRigidityOver,E_CharacterState.DISAPPEAR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}