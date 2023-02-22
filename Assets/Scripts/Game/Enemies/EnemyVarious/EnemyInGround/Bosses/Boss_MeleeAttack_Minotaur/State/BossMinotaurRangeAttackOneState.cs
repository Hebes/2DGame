using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurRangeAttackOneState : BossBaseRangeAttackState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private string _audioClipLandName = "enemy_minotaur_land";
    private string _audioClipName = "enemy_minotaur_rangeAttack1";
    public BossMinotaurRangeAttackOneState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
        SetRangeAttackCombatIndex(_data.rangeAttackOneCombatIndex);//����Զ�̹���������
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void SpawnRangeAttack()
    {
        SpawnBomb();//����ը��
        AudioMgr.Instance.PlayOnce(_audioClipLandName);
        CreateEarthquake();
    }
    //��д����ը����λ��
    protected override Vector3 GetSpawnBombPos()
    {
        var pos = GroundPosUtil.GetPlayrerGroundPos();
        if (pos != Vector3.zero)
            return pos;
        return base.GetSpawnBombPos();
    }
    //����Ч��
    private void CreateEarthquake()
    {
        var shakeTime = _data.rangeAttackShakeArgs.cameraShakeTime;
        var shakeStrength = _data.rangeAttackShakeArgs.cameraShakeStrength;
        var shakeFrequency = _data.rangeAttackShakeArgs.cameraShakeFrequency;
        CameraMgr.Instance.DoCameraNormalShake(shakeTime, shakeStrength, shakeFrequency);
    }
}
