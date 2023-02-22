/****************************************************
    文件：AirBossDemonRangeAttackThreeState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 16:2:52
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossDemonRangeAttackThreeState : AirBossBaseRangeAttackState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private bool _isUp;//是否生产在屏幕的上方
    private string _audioClipName = "enemy_demon_rangeAttack2_rangeAttack3";
    public AirBossDemonRangeAttackThreeState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && CheckCanEnterRangeAttack(), E_CharacterState.RANGEATTACKTHREE);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);

        SetRangeAttackCombatIndex(_data.rangeAttackThreeCombatIndex);
    }
    public override void Enter()
    {
        base.Enter();
        _isUp = !_isUp;
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void SpawnRangeAttack() 
     => CreateScreenBulletUpAndDown(_data.rangeAttackThreeBulletNum, _isUp);
    public override int GetMaxRangeAttackNum()
     => _data.rangeAttackThreeContinueNum;
}