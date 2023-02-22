/****************************************************
    文件：AirBossDemonRangeAttackFourState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 21:41:21
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossDemonRangeAttackFourState : AirBossBaseRangeAttackState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private string _audioClipName = "enemy_demon_rangeAttack4";
    public AirBossDemonRangeAttackFourState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && CheckCanEnterRangeAttack(), E_CharacterState.RANGEATTACKFOUR);
        AddTargetState(() => _isAbilityDown, E_CharacterState.APPEAR);
        SetRangeAttackCombatIndex(_data.rangeAttackFourCombatIndex);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void SpawnRangeAttack()
    {
        CreateScreenBomb(_data.rangeAttackFourBulletNum);
    }
    public override int GetMaxRangeAttackNum()
     => _data.rangeAttackFourContinueNum;
}