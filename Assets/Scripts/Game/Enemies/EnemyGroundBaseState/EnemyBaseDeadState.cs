using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseDeadState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isDead;
    public EnemyBaseDeadState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _isDead = false;
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetXVelocity(0);
        if (!_isDead&&AnimFinish)
        {
            DeadAction();
            _isDead = true;
        }
    }
    protected virtual void DeadAction()
    {
        GameObject.Destroy(_ower.gameObject);
    }
    //创造死亡Sprite
    protected void CreateDeadSprite()
        => SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_CREATEDEADSPRITE);
}
