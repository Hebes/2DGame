/****************************************************
    文件：BossBaseInAirState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 10:0:18
	功能：Boss空中状态基类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBaseInAirState<T, X> : EnemyBaseInAirState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseInAirState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}