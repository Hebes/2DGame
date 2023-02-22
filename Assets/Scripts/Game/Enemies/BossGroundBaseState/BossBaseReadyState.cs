/****************************************************
    文件：BossBaseReadyState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 10:2:16
	功能：Boss基类准备状态
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBaseReadyState<T, X> : EnemyBaseState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseReadyState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        Move.SetXVelocity(0);
    }
}