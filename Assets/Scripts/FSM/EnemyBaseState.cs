using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState<T, X> : FSMBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected EnemyRgMoveComponent _move;
    public new EnemyRgMoveComponent Move
    {
        get
        {
            if (_move == null)
                _move = _core.Get<EnemyRgMoveComponent>();
            return _move;
        }
    }
    protected EnemyColliderCheckComponent _colliderCheck;
    public new EnemyColliderCheckComponent ColliderCheck
    {
        get
        {
            if (_colliderCheck == null)
                _colliderCheck = _core.Get<EnemyColliderCheckComponent>();
            return _colliderCheck;
        }
    }
    protected EnemyBehaviorComponent _behavior;
    protected EnemyBehaviorComponent Behavior
    {
        get
        {
            if (_behavior == null)
                _behavior = _core.Get<EnemyBehaviorComponent>();
            return _behavior;
        }
    }
    protected SubEventMgr SubEventMgr { get; private set; }
    protected Transform PlayerTf => Move.PlayerTf;
    protected Vector3 PlayerPos => PlayerTf==null?Vector3.zero:PlayerTf.position;
    public EnemyBaseState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {
        SubEventMgr = _ower.SubEventMgr;
    }    
    public override void FindAnimAndCore()
    {
        _core = _ower.Core;
        _anim = _ower.Anm;
    }
}
