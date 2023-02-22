using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurInAirState : BossBaseInAirState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private string _audioClipName = "enemy_minotaur_land";
    public BossMinotaurInAirState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isDead,E_CharacterState.DEAD);
        AddTargetState(()=>_isOnGround&&Move.GetCurVelocity.y<=0.01f,E_CharacterState.IDLE);
    }
    public override void Exit()
    {
        base.Exit();
        CreateEarthquake();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    private void CreateEarthquake()
    {
        var shakeTime = _data.inAirOverShakeArgs.cameraShakeTime;
        var shakeStrength = _data.inAirOverShakeArgs.cameraShakeStrength;
        var shakeFrequency = _data.inAirOverShakeArgs.cameraShakeFrequency;
        CameraMgr.Instance.DoCameraNormalShake(shakeTime, shakeStrength, shakeFrequency);
    }
}
