using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurDetectedState : BossBaseDetectedState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private E_AttackState _curAttackState;
    //ËùÓÐµÄ¹¥»÷×´Ì¬
    private enum E_AttackState
    {
        MeleeAttack,
        DashState,
        RangeAttackOne,
        RangeAttackTwo,
        Count
    };
    public BossMinotaurDetectedState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>!_isGrounded,E_CharacterState.INAIR);
        AddTargetState(()=>PlayerTf==null,E_CharacterState.IDLE);
        AddTargetState(() => _inOverMaxWaitTime && _curAttackState == E_AttackState.DashState, E_CharacterState.READY);
        AddTargetState(() => _inOverMaxWaitTime && (_isInMeleeAttack || _curAttackState == E_AttackState.MeleeAttack), E_CharacterState.MELEEATTACK);
        AddTargetState(() => _inOverMaxWaitTime && _curAttackState == E_AttackState.RangeAttackOne, E_CharacterState.RANGEATTACK);
        AddTargetState(() => _inOverMaxWaitTime && _curAttackState == E_AttackState.RangeAttackTwo, E_CharacterState.RANGEATTACKTWO);
    }
    public override void Enter()
    {
        base.Enter();
        RandomNextAttackState();
    }
    private void RandomNextAttackState()
        => _curAttackState = (E_AttackState)Random.Range(0, (int)E_AttackState.Count);
}
