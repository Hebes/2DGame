/****************************************************
    文件：AirEnemyBaseDisappearState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 16:31:33
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AirEnemyBaseDisappearState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirEnemyBaseDisappearState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Exit()
    {
        base.Exit();
        DisappearAction();
    }
    public abstract void DisappearAction();
}