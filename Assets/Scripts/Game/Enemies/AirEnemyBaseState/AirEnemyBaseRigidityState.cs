/****************************************************
    文件：AirEnemyBaseRigidityState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/23 18:35:48
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirEnemyBaseRigidityState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isRigidityOver;
    public AirEnemyBaseRigidityState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {


    }
    public override void Enter()
    {
        base.Enter();
        _isRigidityOver = false;
        Move.SetGravityScale(_data.deadGravitySacle);
        Combat.SetActive(false);
    }
    public override void Exit()
    {
        base.Exit();
        Move.ResetGravityScale();
        Combat.SetActive(true);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        CheckRigidityIsOver();
    }
    //检测僵直状态是否结束
    protected virtual void CheckRigidityIsOver()
    {
        if(!_isRigidityOver&&Time.time>=_enterTime+_data.maxRigidityTime)
            _isRigidityOver = true;
    }
}