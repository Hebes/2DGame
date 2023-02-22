/****************************************************
    文件：EnemyBaseRigidbidyState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/24 9:46:33
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBaseRigidityState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isRigidityOver;//是否僵直结束
    public EnemyBaseRigidityState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {


    }
    public override void Enter()
    {
        base.Enter();
        _isRigidityOver = false;
        Move.SetXVelocity(0);
        Behavior.SetDomineering(true);
        Combat.SetActive(false);
    }
    public override void Exit()
    {
        base.Exit();
        Behavior.SetDomineering(false);
        Combat.SetActive(true);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckRigidityIsOver();
    }
    protected virtual void CheckRigidityIsOver()
    {
        if (!_isRigidityOver && Time.time >= _enterTime + _data.maxRigidityTime)
            _isRigidityOver = true;
    }
}