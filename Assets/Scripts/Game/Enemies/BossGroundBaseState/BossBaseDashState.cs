/****************************************************
    文件：BossBaseDashState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 10:0:31
	功能：Boss冲刺状态基类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBaseDashState<T, X> : EnemyBaseDashState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseDashState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
}