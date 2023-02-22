using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAirAttackLoop : AbilityBaseState
{
    protected bool _isGround;
    protected float _curMovementVelocity;
    public MeleeAirAttackLoop(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Check()
    {
        base.Check();
        _isGround = ColliderCheck.Ground;
    }
    public override void Enter()
    {
        base.Enter();
        Combat.OpenAttack(E_Attack.MELEE_AIRLOOP,SetAbilityDown);
        _curMovementVelocity = Combat.GetCurCombatVelocity();
    }  
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocity(0);
        Move.SetYVelocity(_curMovementVelocity);    
    } 
    public override E_CharacterState TargetState() => E_CharacterState.IDLE;

}
