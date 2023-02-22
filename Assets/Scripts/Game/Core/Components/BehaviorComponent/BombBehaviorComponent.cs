/****************************************************
    文件：BombBehaviorComponent.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/7 13:47:58
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombBehaviorComponent : BehaviorBaseComponent,IBombBehavior
{
    public override void Damage(Vector3 attackPos, float damageValue, E_Attack type = E_Attack.NONE)
    {
        Dead();
    }
    public override void Dead()
    {
        SubEventMgr.ExecuteEvent(E_EventName.BOMB_EXLPOSTION);
    }
    public void RecoverHealth()
        => _curHelath = _maxHealth;
    public void SetGroup(E_Group selfGroup)
        => _group = selfGroup;
}
  