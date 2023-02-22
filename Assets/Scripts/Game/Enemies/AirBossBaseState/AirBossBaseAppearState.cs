/****************************************************
    文件：AirBossBaseAppearState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/23 18:10:22
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossBaseAppearState<T, X> : AirEnemyBaseAppearState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirBossBaseAppearState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}