using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseDeadState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isDead;
    public AirEnemyBaseDeadState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Move.SetGravityScale(_data.deadGravitySacle);
    }
    public override void ActionUpdate()
    {
        //todo需要修改
        base.ActionUpdate();
        Move.SetXVelocity(0);
        if (!_isDead && AnimFinish)
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
    protected virtual void CreateDeadSprite()
    {
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_CREATEDEADSPRITE);
    }
}
