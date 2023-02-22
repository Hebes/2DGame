using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossDemonDetectedState : AirBossBaseDetectedState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{

    private E_RandomRangeAttack _curAttack;
    private enum E_RandomRangeAttack
    {
        RangeAttackOne,
        RangeAttackTwo,
        RangeAttackThree,
        MeleeAttackOne,
        Count
    }
    public AirBossDemonDetectedState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isDetectedOver && PlayerTf == null, E_CharacterState.IDLE);
        AddTargetState(() => _isDetectedOver && !_isPlayerInRangeAttack, E_CharacterState.CHARGE);
        AddTargetState(() => _isDetectedOver && _curAttack == E_RandomRangeAttack.RangeAttackOne, E_CharacterState.RANGEATTACK);
        AddTargetState(() => _isDetectedOver && _curAttack == E_RandomRangeAttack.RangeAttackTwo, E_CharacterState.RANGEATTACKTWO);
        AddTargetState(() => _isDetectedOver && _curAttack == E_RandomRangeAttack.RangeAttackThree, E_CharacterState.RANGEATTACKTHREE);
        AddTargetState(() => _isDetectedOver && _curAttack == E_RandomRangeAttack.MeleeAttackOne, E_CharacterState.MELEEATTACK);
    }
    public override void Enter()
    {
        base.Enter();
        GetCurAttack();
    }
    private void GetCurAttack()
      => _curAttack = (E_RandomRangeAttack)Random.Range(0, (int)E_RandomRangeAttack.Count);
}
