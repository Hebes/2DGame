/****************************************************
    文件：AirBossBaseDisappearState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/23 18:10:38
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AirBossBaseDisappearState<T, X> : AirEnemyBaseDisappearState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseDisappearState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}