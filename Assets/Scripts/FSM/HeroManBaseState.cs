using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroManBaseState : FSMBaseState<HeroMan, HeroManData>
{
    protected SubEventMgr SubEventMgr { get; private set; }
    private HeroBehaviorComponent _behavior;
    public HeroBehaviorComponent Behavior
    {
        get
        {
            if (_behavior == null)
                _behavior = _core.Get<HeroBehaviorComponent>();
            return _behavior;
        }
    }
    protected HeroCombatComponent _combat;
    protected new HeroCombatComponent Combat
    {
        get
        {
            if (_combat == null)
                _combat = _core.Get<HeroCombatComponent>();
                return _combat;
        }
    }
    public HeroManBaseState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        SubEventMgr = _ower.SubEventMgr;
    }
    public sealed override void FindAnimAndCore()
    {
        _anim = _ower.Anim;
        _core = _ower.Core;
    }
}
