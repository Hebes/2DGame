/****************************************************
    文件：BossBaseRigidityState.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/4 10:3:36
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBaseRigidityState<T, X> : EnemyBaseRigidityState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public BossBaseRigidityState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isRigidityOver,E_CharacterState.MELEEATTACKTWO);
    }
}