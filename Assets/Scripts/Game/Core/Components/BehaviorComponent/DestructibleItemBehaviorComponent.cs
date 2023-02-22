using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItemBehaviorComponent:BehaviorBaseComponent
{
    public override void Damage(Vector3 attackPos, float damageValue, E_Attack type = E_Attack.NONE)
    {
        if (_curHelath == -1) return;
        //如果处于无敌状态
        if (Time.time < _lastHitTime + _invincibleTime) return;
        _lastHitTime = Time.time;
        CreateHitEffect(_hitDir.x > 0 ? 1 : -1);
        //处于霸体状态则不会受伤
        _curHelath -= damageValue;
        //如果死亡就不触发受伤状态了
        if (_curHelath <= 0)
        {
            Dead();
            return;
        }
        _core.SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_HIT);
    }
    public override void Dead()
    {
        _curHelath = -1;
        SetCollider(false);
        CreateDeadEffect();
        _core.SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_DEAD);
    }
}
