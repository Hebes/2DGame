/****************************************************
    文件：BossMinotaurRangeAttackTwoState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 16:44:53
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurRangeAttackTwoState : BossBaseRangeAttackState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private string _audioClipName = "enemy_minotaur_rangeAttack2";
    public BossMinotaurRangeAttackTwoState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
        SetRangeAttackCombatIndex(_data.rangeAttackTwoCombatIndex);//设置远程攻击的索引
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void SpawnRangeAttack()
    {
        CreateLargetBulletOfSector(_data.rangeAttackOneBulletNum,_data.rangeAttackOneOffselAngel);
    }
}