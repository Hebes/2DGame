using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurHitState : BossBaseHitState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private bool _canEnterBackState;
    private bool _canEnterMeleeAttackTwoState;
    private string _audioClipName = "enemy_minotaur_hit";
    public BossMinotaurHitState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_canEnterMeleeAttackTwoState||_isWallBack,E_CharacterState.READY);
        AddTargetState(()=>_canEnterBackState,E_CharacterState.BACK);
        AddTargetState(()=>AnimFinish,E_CharacterState.DETECTED);
    }
    public override void Enter()
    {
        base.Enter();
        CheckCanEnterMeleeAttackTwoState();
        CheckCanEnterRangeAttackTwoState();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    //检测是否可以进入后退状态
    private void CheckCanEnterRangeAttackTwoState()
        => _canEnterBackState = Random.Range(0, 100) < _data.hitToBackStateProbability;
    //检测是否可以进入MeleeAttackTwo状态
    private void CheckCanEnterMeleeAttackTwoState()
      => _canEnterBackState = Random.Range(0, 100) < _data.hitToReadyProbability;
}
