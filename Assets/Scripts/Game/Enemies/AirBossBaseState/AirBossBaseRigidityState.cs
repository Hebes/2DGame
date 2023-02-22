/****************************************************
    文件：AirBossBaseRigidityState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/23 18:51:55
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossBaseRigidityState<T, X> : AirEnemyBaseRigidityState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseRigidityState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}