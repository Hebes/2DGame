/****************************************************
    文件：BossBaseMeleeAttackState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 10:4:1
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBaseMeleeAttackState<T, X> : EnemyBaseMeleeAttackState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseMeleeAttackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}