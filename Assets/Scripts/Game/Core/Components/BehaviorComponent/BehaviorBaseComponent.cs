using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
//敌人和主角受伤组件判断基类
//判断敌人和主角的受伤和死亡
public class BehaviorBaseComponent : ComponentBase, IBehavior
{
    [TabGroup("相关特效预制体"), SerializeField]
    protected GameObject _hitEffect;
    [TabGroup("相关特效预制体"), SerializeField]
    protected GameObject _deadEffect;

    [TabGroup("相关数值设置"), SerializeField]
    protected float _maxHealth;//当前最大血量
    protected float _curHelath;
    [TabGroup("相关数值设置"), SerializeField]
    protected float _invincibleTime = 0.4f;//无敌时间
    protected float _lastHitTime;
    [TabGroup("相关数值设置"), SerializeField]
    protected float _bulletTime;
    public Vector2 _hitDir { get; protected set; }
    [TabGroup("相关数值设置")]
    public float _strength;//hit时产生的力
    protected Collider2D _collider;
    protected bool _domineering;//是否无敌
    [TabGroup("相关阵营设置"), SerializeField, EnumToggleButtons, HideLabel]
    protected E_Group _group;//自己属于的阵营

    protected bool _isShieldState;//是否处于举盾状态
    protected int FacingDir;

    public override void Init()
    {
        base.Init();
        InitHealth();
        _collider = Get<Collider2D>();
        if (_collider != null)
            _collider.isTrigger = true;
    }
    protected virtual void InitHealth()
        => _curHelath = _maxHealth;
    public virtual void Damage(Vector3 attackPos, float damageValue, E_Attack type = E_Attack.NONE)
    {
        if (_curHelath == -1) return;
        //如果处于无敌状态
        if (Time.time < _lastHitTime + _invincibleTime) return;
        _lastHitTime = Time.time;
        OpenBulleTime();
        //处于霸体状态则不会受伤
        if (_domineering)
        {
            _core.SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_DOMINEERING);
            return;
        }
        _curHelath -= damageValue;
        _hitDir = (_owerTf.transform.position - attackPos).normalized;
        //如果死亡就不触发受伤状态了
        if (_curHelath <= 0)
        {
            Dead();
            return;
        }
        CreateHitEffect(_hitDir.x>0?1:-1);
        _core.SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_HIT);
    }
    //创造受伤特效
    protected virtual void CreateHitEffect(int hitDir)
    {
        if (_hitEffect == null)
            return;
        var hitEffectGo = PoolManager.Instance.GetFromPool(_hitEffect);
        hitEffectGo.transform.position = transform.position;
        var euler = hitEffectGo.transform.localEulerAngles;
        euler.x = hitDir == 1 ? 0f : 180f;
        hitEffectGo.transform.localEulerAngles = euler;
        hitEffectGo.AddOrGet<AutoRecycleComponent>().Init(1f);
    }
    protected virtual void CreateDeadEffect()
    {
        if (_deadEffect == null)
            return;
        var hitEffectGo = PoolManager.Instance.GetFromPool(_hitEffect);
        hitEffectGo.transform.position = transform.position;
        hitEffectGo.transform.rotation = Quaternion.identity;
        hitEffectGo.AddOrGet<AutoRecycleComponent>().Init(1f);
    }
    public virtual void Dead()
    {
        _curHelath = -1;
        CreateDeadEffect();
        _core.SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_DEAD);
    }
    public virtual E_Group GetGroup()
        => _group;
    public void SetActive(bool value)
        => gameObject.SetActive(value);
    //设置是否无敌
    public void SetDomineering(bool value)
        => _domineering = value;
    //开启受伤显示的子弹时间
    protected void OpenBulleTime()
    {
        if (_bulletTime != 0)
            TimeScaleMgr.Instance.OpenSlowOutBulletTime(_bulletTime, _bulletTime);
    }
    public virtual void RecoverHp(int value, bool isAll = false)
    {
        if (isAll)
            _curHelath = _maxHealth;
        else
        {
            _curHelath += Mathf.Abs(value);
            if (_curHelath >= _maxHealth)
                _curHelath = _maxHealth;
        }
    }
    //得到当前健康的百分比
    public int GetCurHealth()
        => (int)(_curHelath / _maxHealth * 100);
    //设置是否处于举盾状态
    public void SetShieldState(bool value)
        => _isShieldState = value;
    public bool GetShieldState()
        => _isShieldState;
    //得到所属主物体的面朝向
    public int GetOwerFacingDir()
        => _owerTf.localEulerAngles.y == 0 ? 1 : -1;
    //受到击退
    public void BeatBack()
        => SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_BEATBACK);
    protected void SetCollider(bool value)
        =>_collider.enabled=value;
}
