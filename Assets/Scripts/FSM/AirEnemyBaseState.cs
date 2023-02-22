using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    private  new AirEnemyColliderCheckComponent _colliderCheck;
    public new AirEnemyColliderCheckComponent ColliderCheck
    {
        get
        {
            if (_colliderCheck == null)
                _colliderCheck = _core.Get<AirEnemyColliderCheckComponent>();
            return _colliderCheck;
        }
    }
    private NavFindingComponent _navFindingComponent;
    public NavFindingComponent NavFindingComponent
    {
        get
        {
            if (_navFindingComponent == null)
                _navFindingComponent = _core.Get<NavFindingComponent>();
            return _navFindingComponent;
        }
    }
    public AirEnemyBaseState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {


    }
}
