/****************************************************
    文件：HeroCombatComponent.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/15 10:33:8
	功能：英雄战斗组件
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using System;

public class HeroCombatComponent : CombatComponent
{
    [BoxGroup("战斗组件设置"), SerializeField, LabelWidth(150)]
    private float _maxMagicPower = 50;//最大魔法值
    [BoxGroup("战斗组件设置"), SerializeField, LabelWidth(150)]
    private float _maxLightPower = 50;
    [BoxGroup("战斗组件设置"), SerializeField, LabelWidth(150)]
    private float _lightReduceSpeed = 5f;
    [BoxGroup("战斗组件设置"), SerializeField, LabelWidth(150)]
    private float _lightReduceDelayTime = 10f;//开始减少当前光源值的延迟时间
    [BoxGroup("战斗组件设置"), SerializeField, LabelWidth(150)]
    private float _attackNormalAddLightValue = 5;//成功普通攻击增加的光源值
    [BoxGroup("战斗组件设置"), SerializeField, LabelWidth(150)]
    private float _attackParryAddLightValue = 10;//成功弹反攻击增加的光源值
    private float _curMagicPower;//当前魔法值
    private float _curLightPower;//当前光源值

    private float _lastAddLightPowerTime;//上次增加了光源值的时间
    private float _lastReduceLightPowerTime;//上次减少了光源值的时间


    private float _lightValueRatio = 1f;
    private float _improveRecoverTimer;
    private float _improveRevoverTime;
    private bool _isImproveStart;
    private ShowHurtMaterialComponent _showHurtComponent;


    public override void Init()
    {
        _lastAddLightPowerTime = Time.unscaledTime;
        var setting = GameDataModel.Instance.GetHeroManSettingData;
        float attack = setting.attack;
        float maxMagicPower = (float)setting.maxMagicPower;
        float lightReduceSpeed = (float)setting.lightReduceSpeed;
        float lightAddRatio = (float)setting.lightAddRatio;
        InitAttack(attack);//需要在前面进行初始化
        InitMaxMagicPower(maxMagicPower);
        InitLightPowerValue(lightReduceSpeed, lightAddRatio);
        RecoverMagicPower(maxMagicPower, true);
        RecoverLightPower(0, false);
        base.Init();
        if (UIManager.Instance.CheckExistPanel(Paths.PREFAB_UIPANEL_GAMEPANEL))
        {
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESHPLAYERLIGHT, _curLightPower / _maxLightPower);
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESHPLAYERMAGIC, _curMagicPower / _maxMagicPower);
        }
        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_ADDATTACK, AddAttack);
        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_ADDMAXMAGICPOWER, AddMaxMagicPower);
        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_ADDMAGICPOWER, AddCurMaigcPower);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_ADDATTACK, AddAttack);
        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_ADDMAXMAGICPOWER, AddMaxMagicPower);
        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_ADDMAGICPOWER, AddCurMaigcPower);
    }
    private void AddAttack(params object[] args)
    {
        var setting = GameDataModel.Instance.GetHeroManSettingData;
        float attack = setting.attack;
        InitAttack(attack);
        _combatData.Init();
    }
    private void AddCurMaigcPower(params object[] args)
    {
        float value = (float)args[0];
        ChangeCurMagicPower(value);
    }
    private void AddMaxMagicPower(params object[] args)
    {
        var setting = GameDataModel.Instance.GetHeroManSettingData;
        float maxMagicPower = (float)setting.maxMagicPower;
        InitMaxMagicPower(maxMagicPower);
    }
    protected override void InitComponent()
    {
        base.InitComponent();
        _showHurtComponent = gameObject.AddOrGet<ShowHurtMaterialComponent>();
        _showHurtComponent.Init(_render, Consts.CHARACTER_SHOWHURTEFFECT_TIME * 2);
    }
    protected override void Update()
    {
        base.Update();
        CheckChangeLightPower();
        CheckImproveIsOver();
    }
    //初始化玩家攻击力
    private void InitAttack(float attack)
    {
        for (int i = 0; i < _combatData.attackDetails.Length; i++)
            _combatData.attackDetails[i].damageAmount = attack;
    }
    private void InitMaxMagicPower(float maxValue)
        => _maxMagicPower = maxValue;
    private void InitLightPowerValue(float reduceSpeed, float addRatio)
    {
        _lightReduceSpeed = reduceSpeed;
        _attackNormalAddLightValue *= addRatio;
        _attackParryAddLightValue *= addRatio;
    }
    public void RecoverMagicPower(float value, bool isAll = false)
    {
        if (isAll)
            value = _maxMagicPower;
        else
            value = Mathf.Abs(value);
        ChangeCurMagicPower(value);
    }
    public void RecoverLightPower(float value, bool isAll = false)
    {
        if (isAll)
            value = _maxLightPower;
        else
            value = Mathf.Abs(value);
        _lastAddLightPowerTime = Time.unscaledTime;
        ChangeCurLightPower(value);
        SubEventMgr?.ExecuteEvent(E_EventName.CHARACTER_CHECKCANRECOVER);
    }
    public bool CheckCanRangeAttack(float value)
    {
        var curValue = _curMagicPower;
        if (curValue + value < 0)
            return false;
        ChangeCurMagicPower(value);
        return true;
    }
    //改变当前魔法值
    public void ChangeCurMagicPower(float value)
    {
        var curValue = _curMagicPower;
        curValue += value;
        curValue = Mathf.Clamp(curValue, 0, _maxMagicPower);
        _curMagicPower = curValue;
        if (UIManager.Instance.CheckExistPanel(Paths.PREFAB_UIPANEL_GAMEPANEL))
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESHPLAYERMAGIC, _curMagicPower / _maxMagicPower);
    }
    //改变当光源值
    public void ChangeCurLightPower(float value)
    {
        var curValue = _curLightPower;
        curValue += value;
        curValue = Mathf.Clamp(curValue, 0, _maxLightPower);
        _curLightPower = curValue;
        if (UIManager.Instance.CheckExistPanel(Paths.PREFAB_UIPANEL_GAMEPANEL))
            EventMgr.Instance.ExecuteEvent(E_EventName.UI_REFRESHPLAYERLIGHT, _curLightPower / _maxLightPower);
    }
    //检测改变光源值
    private void CheckChangeLightPower()
    {
        if (Time.unscaledTime >= _lastAddLightPowerTime + _lightReduceDelayTime && _curLightPower > 0)
        {
            if (Time.unscaledTime >= _lastReduceLightPowerTime + 1f && _curLightPower > 0)
            {
                ChangeCurLightPower(-_lightReduceSpeed);
                _lastReduceLightPowerTime = Time.unscaledTime;
            }
        }
    }
    protected override void TakeDamage(IBehavior behavior, AttackDetails attackDeails, Collider2D collider,bool isBullet=false)
    {
        var damageValue = attackDeails.damageAmount;
        //如果是远程攻击 且 包含默认攻击 则攻击伤害为默认攻击的伤害
        if (IsRangeAttackType(attackDeails.attackType) && GetAllAttackType().Contains(E_Attack.NONE))
            damageValue = GetNoneAttackTypeDamageValue();
        if (damageValue > 0)
        {
            //如果敌人处于护盾状态 且 与敌人面对面 则无法造成伤害
            if (behavior.GetShieldState() && behavior.GetOwerFacingDir() == -GetOwerFacingDir())
            {
                AudioMgr.Instance.PlayOnce("hitShield");
                behavior.BeatBack();
            }
            else
            {
                if (!isBullet)
                    behavior.Damage(_owerTf.transform.position, damageValue, _curType);
                RecoverLightPower((_isHit ? _attackParryAddLightValue : _attackNormalAddLightValue) * _lightValueRatio);
            }
            AttackEffect(attackDeails, collider);
            _isHit = false;
        }
    }
    //检测是否可以恢复生命值
    public bool CheckCanRecoverHp()
    {
        bool value = _curLightPower >= _maxLightPower;
        if (value)
        {
            ChangeCurLightPower(-_curLightPower);
        }
        return value;
    }
    protected override void ShowHitEffect(params object[] args)
    {
        _isHit = true;
        _showHurtComponent.ShowHitEffect();
    }
    protected override void CreateMeleeAttackEffect(Collider2D collider)
    {
        GameObject effectGo;
        if (collider.CompareTag(Tags.Enemy))
            effectGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_SWORDHITENEMY);
        //如果不是敌人 是子弹 可摧毁物体
        else
            effectGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_SWORDHITBULLET);
        effectGo.transform.position = collider.bounds.ClosestPoint(transform.position);
        effectGo.transform.rotation = Quaternion.identity;
        effectGo.AddOrGet<AutoRecycleComponent>().Init(1f);
    }
    //提高恢复light的能力
    public void ImproveRecoverHp(float targetRatio, float time)
    {
        _lightValueRatio = targetRatio;
        _improveRevoverTime = time;
        _improveRecoverTimer = 0;
        _isImproveStart = true;
    }
    //检测恢复能力是否结束
    private void CheckImproveIsOver()
    {
        if (!_isImproveStart)
            return;
        if (_improveRecoverTimer < _improveRevoverTime)
            _improveRecoverTimer += Time.deltaTime;
        else
        {
            _improveRecoverTimer = 0;
            _lightValueRatio = 1f;
            _isImproveStart = false;
            EventMgr.Instance.ExecuteEvent(E_EventName.USEITEM_IMPROVERECOVER_TEMPORARY,false);
        }
    }
}