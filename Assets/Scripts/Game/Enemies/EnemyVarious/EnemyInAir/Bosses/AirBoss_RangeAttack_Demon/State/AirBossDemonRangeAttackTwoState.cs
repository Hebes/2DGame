/****************************************************
    文件：AirBossDemonRangeAttackTwoState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 16:2:23
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossDemonRangeAttackTwoState : AirBossBaseRangeAttackState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    protected bool _isRight;//当前生成的子弹的方向
    private string _audioClipName = "enemy_demon_rangeAttack2_rangeAttack3";
    public AirBossDemonRangeAttackTwoState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && CheckCanEnterRangeAttack(), E_CharacterState.RANGEATTACKTWO);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
        SetRangeAttackCombatIndex(_data.rangeAttackTwoCombatIndex);
    }
    public override void Enter()
    {
        base.Enter();
        _isRight = !_isRight;
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void SpawnRangeAttack()
        => CreateScreenBulletLeftAndRight(_data.rangeAttackTwoBulletNum, _isRight);
    public override int GetMaxRangeAttackNum()
        => _data.rangeAttackTwoContinueNum;
}