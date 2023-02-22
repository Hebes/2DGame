/****************************************************
    文件：BossBaseBackState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 10:2:37
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBaseBackState<T, X> : EnemyBaseBackState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseBackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}