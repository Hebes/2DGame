using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurMeleeAttackTwoState : BossBaseMeleeAttackState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    protected bool _isCanEenterMeleeAttackOne;//是否可以在冲刺状态下攻击
    private string _audioClipName = "enemy_minotaur_dash";
    public BossMinotaurMeleeAttackTwoState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && _isInMeleeAttack && _isCanEenterMeleeAttackOne, E_CharacterState.MELEEATTACK);
        AddTargetState(() => _isAbilityDown, E_CharacterState.RANGEATTACKTWO);
    }
    public override void Enter()
    {
        SetAttackCombatIndex(_data.meleeAttackTwoCombatIndex);//需要重复设置
        base.Enter();
        _isCanEenterMeleeAttackOne = Random.Range(0, 100) < _data.meleeAttackTwoToAttackOneProbability;
        CreateEarthquake();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocityInFacing(_data.meleeAttackTwoVelocity, true);
    }
    private void CreateEarthquake()
    {
        var shakeTime = _data.meleeAttackTwoShakeArgs.cameraShakeTime;
        var shakeStrength = _data.meleeAttackTwoShakeArgs.cameraShakeStrength;
        var shakeFrequency = _data.meleeAttackTwoShakeArgs.cameraShakeFrequency;
        CameraMgr.Instance.DoCameraNormalShake(shakeTime, shakeStrength, shakeFrequency);
    }
}
