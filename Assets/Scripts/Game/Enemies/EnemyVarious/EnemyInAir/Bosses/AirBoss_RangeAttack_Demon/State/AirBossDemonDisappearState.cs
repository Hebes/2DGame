using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossDemonDisappearState : AirBossBaseDisappearState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    protected bool _isImmediateaToAppearState;
    private string _audioClipName = "enemy_demon_disappear";
    public AirBossDemonDisappearState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => AnimFinish && _isImmediateaToAppearState, E_CharacterState.APPEAR);
        AddTargetState(() => AnimFinish, E_CharacterState.RANGEATTACKFOUR);
    }
    
    //�ú�����Exit��ִ��
    public override void DisappearAction()
    {
        _ower.Render.enabled = false;
    }
    public override void Enter()
    {
        base.Enter();
        Behavior.SetActive(false);
        _isImmediateaToAppearState = false;
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    //�����Ƿ���������AppearState
    public void SetIsisImmediateaToAppearState(bool value)
        => _isImmediateaToAppearState = value;
}
