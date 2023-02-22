/****************************************************
    文件：AirEnemyBaseAppearState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 16:31:16
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirEnemyBaseAppearState<T, X> : AirEnemyBaseState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    public AirEnemyBaseAppearState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AppearAction();
    }
    protected virtual void AppearAction()
    {
        _ower.transform.position = GetPos();
    }
    protected virtual Vector3 GetPos()
        => ScreenPosUtil.GetScreenCenterPos();
}