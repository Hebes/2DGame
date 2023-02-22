using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBehaviorComponent : BehaviorBaseComponent
{
    private string _audioEnemyDamageClipName = "enemy_damage";
    private string _audioEnemyDeadClipName = "enemy_dead";


    [TabGroup("相关数值设置"), LabelWidth(160), SerializeField]
    public Vector2 _enemyHitDir = new Vector2(1, 0);
    [TabGroup("相关数值设置"), LabelWidth(160), SerializeField, Range(0, 100)]
    private float _hitToRigidityStateProcent = 100f;//每损失多少血量百分则进入僵直状态
    private float _lastToRigidityStateHealthProcent = 100f;//上一次进入僵直的状态的血量百分比
    public override void Damage(Vector3 attackPos, float damageValue, E_Attack type = E_Attack.NONE)
    {
        if (_curHelath == -1) return;
        //如果处于无敌状态
        if (Time.time < _lastHitTime + _invincibleTime) return;
        OpenBulleTime();
        //敌人霸体状态依然要受伤
        _curHelath -= damageValue;
        _lastHitTime = Time.time;
        //如果死亡就不触发其他状态了
        if (_curHelath <= 0)
        {
            AudioMgr.Instance.PlayOnce(_audioEnemyDeadClipName);
            Dead();
            return;
        }
        AudioMgr.Instance.PlayOnce(_audioEnemyDamageClipName);
        CreateHitEffect(_hitDir.x > 0 ? 1 : -1);
        //进入僵直状态
        if (_lastToRigidityStateHealthProcent - GetCurHealth() > _hitToRigidityStateProcent)
        {
            _lastToRigidityStateHealthProcent = GetCurHealth();
            SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_RIGIBIDY);
            return;
        }
        if (_domineering)
        {
            SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_DOMINEERING);
            _hitDir = Vector3.zero;
        }
        else
        {
            SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_HIT);
            _hitDir = (_owerTf.transform.position - attackPos).normalized;
        }
    }
}
