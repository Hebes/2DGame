using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviorComponent : BehaviorBaseComponent,IBulletBehavior
{
    public override void Damage(Vector3 attackPos, float damageValue, E_Attack type = E_Attack.NONE)
    {
        if (_curHelath == -1) return;
        _lastHitTime = Time.time;
        _curHelath -= damageValue;
        CreateHitEffect(_hitDir.x > 0 ? 1 : -1);
        if (Time.time < _lastHitTime + _invincibleTime)
            _core.SubEventMgr.ExecuteEvent(E_EventName.BULLET_HIT);
        if (_curHelath <= 0)
            Dead();
    }
    public override void Dead()
    {
        _curHelath = -1;
        CreateDeadEffect();
        _core.SubEventMgr.ExecuteEvent(E_EventName.BULLET_KNOCK);
    }
    public void Parry(E_Group group, HashSet<E_Group> hostilityGroup)
        => _core.SubEventMgr.ExecuteEvent(E_EventName.BULLET_REVERSEDIR, group, hostilityGroup);
    public void SetGroup(E_Group selfGroup)
        => _group = selfGroup;
    public void RecoverHealth()
        => _curHelath = _maxHealth;
}
