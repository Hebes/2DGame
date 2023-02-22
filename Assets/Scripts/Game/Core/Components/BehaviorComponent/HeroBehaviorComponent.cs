using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviorComponent : BehaviorBaseComponent
{
    private bool _isShield = false;
    public override void Init()
    {
        base.Init();
        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_ADDMAXHEALTH, AddMaxHealth);
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_ADDMAXHEALTH, AddMaxHealth);
    }
    private void Update()
    {
        CheckInvincibleOver();
    }
    protected override void InitHealth()
    {
        var setting = GameDataModel.Instance.GetHeroManSettingData;
        _maxHealth = setting.maxHealth;
        base.InitHealth();
        if (UIManager.Instance.CheckExistPanel(Paths.PREFAB_UIPANEL_GAMEPANEL))
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESHPLAYERHP, _curHelath, _maxHealth);
    }
    private void AddMaxHealth(params object[] args)
    {
        var setting = GameDataModel.Instance.GetHeroManSettingData;
        _maxHealth = setting.maxHealth;
    }
    public override void Damage(Vector3 attackPos, float damageValue, E_Attack type = E_Attack.NONE)
    {
        if (_curHelath == -1) return;
        //处于霸体状态则不会受伤
        if (_domineering && type != E_Attack.TRAP)
        {
            _core.SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_DOMINEERING);
            AudioMgr.Instance.PlayOnce("hero_reflection");
            OpenBulleTime();
            return;
        }
        //如果处于无敌状态
        if (Time.time < _lastHitTime + _invincibleTime) return;
        _curHelath -= damageValue;
        _lastHitTime = Time.time;
        OpenBulleTime();
        EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESHPLAYERHP, _curHelath, _maxHealth);
        _hitDir = (_owerTf.transform.position - attackPos).normalized;
        //如果死亡就不触发受伤状态了
        if (_curHelath <= 0)
        {
            Dead();
            return;
        }
        _isShield = true;
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_SETSHIELD, true);
        CreateHitEffect(_hitDir.x > 0 ? 1 : -1);
        _core.SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_HIT, type == E_Attack.TRAP);
    }
    //判断当前是否无敌状态已经结束
    public void CheckInvincibleOver()
    {
        if (_isShield && Time.time >= _lastHitTime + _invincibleTime)
        {
            _isShield = false;
            SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_SETSHIELD, false);
        }
    }
    public override void RecoverHp(int value, bool isAll = false)
    {
        base.RecoverHp(value, isAll);
        if (UIManager.Instance.CheckExistPanel(Paths.PREFAB_UIPANEL_GAMEPANEL))
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESHPLAYERHP, _curHelath, _maxHealth);
    }
}
