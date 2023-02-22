using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBaseState : HeroManBaseState
{
    protected bool _isApplyDown;//是否能力已经运用
    public AbilityBaseState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isApplyDown,TargetState());
    }
    public override void Enter()
    {
        base.Enter();
        _isApplyDown = false;
    }  
    public abstract E_CharacterState TargetState();
    //设置能力是否已经应用
    public virtual void SetAbilityDown() => _isApplyDown = true;
}
