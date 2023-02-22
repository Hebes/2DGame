using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurBackState : BossBaseBackState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    public BossMinotaurBackState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isBackOver,E_CharacterState.RANGEATTACK);
    }
    public override void Enter()
    {
        base.Enter();
        Behavior.SetDomineering(true);
    }
    public override void Exit()
    {
        base.Exit();
        Behavior.SetDomineering(false);
    }
}
