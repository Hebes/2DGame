using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
//处理攻击发送的组件
//和Behavior成对的组件  即消息发送者 Behavior是消息接受者
//存储当前的攻击信息的组件(又或者说武器信息
public class CombatComponent : ComponentBase, ICombat
{
    private bool _isAlwaysActive;//是否一直开启
    [SerializeField]
    protected CombatData _combatData;
    [BoxGroup("战斗组件设置"), SerializeField, LabelWidth(150)]
    protected float _comboContinueTime;//可以连击的持续时间
    protected int _curCombatIndex;
    protected float _lastComboTime;//上一次的攻击时间
    protected bool _isAttacking;//是否正在攻击
    protected bool _isParry;
    protected E_Attack _curType;//当前攻击类型
    protected Animator _anim;
    protected SpriteRenderer _render;
    [BoxGroup("战斗组件设置"), SerializeField, EnumToggleButtons]
    protected E_Group[] _hostileGroups;
    protected HashSet<E_Group> _hostileGroupHash;
    protected Queue<Action> _actionQueue = new Queue<Action>();
    //受伤特效显示组件
    protected Collider2D _collider;
    protected int FacingDir;
    protected bool _isHit = false;
    public override void Init()
    {
        base.Init();
        InitComponent();
        InitCombat();
        AddEvent();
    }
    private void OnDestroy()
    {
        RemoveEvent();
    }
    protected virtual void Update()
    {
        CheckCanComboContinue();
    }
    private void OnDisable()
    {
        ExcuteAllAction();
        ResetCurAttackType();
        SetActiveCollider(false);
        AttackOver();
    }
    //初始化额外组件
    protected virtual void InitComponent()
    {
        if (_combatData == null)
            Debug.LogError($"当前战斗组件没有战斗数据,其持有对象为：{_core.transform.name}");
        _combatData.Init();
        _anim = Get<Animator>();
        _hostileGroupHash = new HashSet<E_Group>();
        _render = Get<SpriteRenderer>();
        _collider = Get<Collider2D>();
    }
    //初始化战斗组件
    private void InitCombat()
    {
        if (_combatData.attackDetails == null)
        {
            SetActive(false);
            return;
        }
        foreach (var group in _hostileGroups)
            _hostileGroupHash.Add(group);
        _curCombatIndex = 1;
        _isAttacking = false;
        CheckIsAlwaysActive();
        SetActiveCollider(false);
        ResetCurAttackType();
    }
    private void AddEvent()
    {
        SubEventMgr.AddEvent(E_EventName.CHARACTER_HIT, Hit);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_HITOVER, HitOver);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_DOMINEERING, ShowHitEffect);
    }
    private void RemoveEvent()
    {
        SubEventMgr?.RemoveEvent(E_EventName.CHARACTER_HIT, Hit);
        SubEventMgr?.RemoveEvent(E_EventName.CHARACTER_HITOVER, HitOver);
        SubEventMgr?.RemoveEvent(E_EventName.CHARACTER_DOMINEERING, ShowHitEffect);
    }
    //设置敌对阵营
    public void SetHostileGroupHash(HashSet<E_Group> hostileGroup)
        => _hostileGroups = hostileGroup.ToArray();
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var behavior = collision.GetComponent<IBehavior>();
        var attackDeails = GetCurAttackDetails();
        if (behavior != null
            && GetHostileGroup().Contains(behavior.GetGroup()))
        {
            _isParry = false;
            var bullet = collision.GetComponent<IBulletBehavior>();
            //对方必须为子弹 且 当前攻击类为近战攻击的类型 且没有攻击过
            if (bullet != null && IsMeleeAttackType(_curType))
            {
                SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_PARRY, bullet);
                AudioMgr.Instance.PlayOnce("hero_less_reflection");
                _isParry = true;
            }
            TakeDamage(behavior, attackDeails, collision, bullet == null ? false : true);
        }
    }
    protected virtual void TakeDamage(IBehavior behavior, AttackDetails attackDeails, Collider2D collider, bool isBullet = false)
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
            }
            AttackEffect(attackDeails, collider);
            _isHit = false;
        }
    }
    #region 攻击相关
    //检测是否可以连击
    public void CheckCanComboContinue()
    {
        if (_curCombatIndex > 1 && Time.time >= _lastComboTime + _comboContinueTime)
            ResetCombatIndex();
    }
    //动画是否播放完毕
    public void AnimationFinishTrigger()
    {
        ExcuteAllAction();
        ResetCurAttackType();
    }
    //还远当前攻击索引
    private void ResetCombatIndex()
       => _curCombatIndex = 1;
    //还原当前攻击类型
    private void ResetCurAttackType()
    {
        //优先NONE的攻击类型
        if (GetAllAttackType().Contains(E_Attack.NONE))
            _curType = E_Attack.NONE;
        else
            _curType = _combatData.GetFirstAttackType();
    }
    //检测是否超出索引范围
    private void CheckInOverOfIndex()
    {
        if (_curCombatIndex > _combatData.GetMaxCombatIndex(_curType))
            ResetCombatIndex();
    }
    //开启当前攻击
    public int OpenAttack(E_Attack type, Action action = null, int combatIndex = 0)
    {
        if (_isAttacking)
            return -1;
        if (_curType != type)
            _curType = type;
        if (action != null)
            _actionQueue.Enqueue(action);
        _anim.SetTrigger(type.ToString());
        if (combatIndex != 0)
            _curCombatIndex = combatIndex;
        CheckInOverOfIndex();
        _anim.SetInteger(Consts.CHARACTER_ANM_COMBAT_INDEX, _curCombatIndex);
        _isAttacking = true;
        _lastComboTime = Time.time;
        return _curCombatIndex++;
    }
    //结束当前攻击
    public void AttackOver()
        => _isAttacking = false;
    public HashSet<E_Group> GetHostileGroup()
        => _hostileGroupHash;
    public float GetCurCombatVelocity()
      => GetCurAttackDetails().movemenVelocity;
    public void SetActive(bool value)
        => gameObject.SetActive(value);
    private void ExcuteAllAction()
    {
        for (int i = 0; i < _actionQueue.Count; i++)
            _actionQueue.Dequeue()?.Invoke();
    }
    private int GetCurAttackIndex()
    {
        var value = _anim.GetInteger(Consts.CHARACTER_ANM_COMBAT_INDEX) - 1;
        if (value < 0)
            value = 0;
        return value;
    }
    public void SetActiveCollider(bool value, bool isMust = false)
    {
        if (!value && _isAlwaysActive && !isMust)
            return;
        gameObject.GetComponent<Collider2D>().enabled = value;
    }
    //检测是否开启持续不关闭触发检测
    private void CheckIsAlwaysActive()
    {
        var types = GetAllAttackType();
        //如果包含不是具体的攻击类型  
        if (types.Contains(E_Attack.NONE))
            _isAlwaysActive = true;
    }
    //得到远程攻击对应的发射的预制体子弹
    public GameObject GetRangeAttackPrefab()
        => GetCurAttackDetails().rangeAttackBullet;
    protected AttackDetails GetCurAttackDetails()
        => _combatData.GetAttackDeails(_curType, GetCurAttackIndex());
    protected HashSet<E_Attack> GetAllAttackType()
        => _combatData.GetCombatDataAllType();
    protected float GetNoneAttackTypeDamageValue()
        => _combatData.GetAttackDeails(E_Attack.NONE, 0).damageAmount;
    //得到当前Combat组件的状态
    public bool GetCombatActive()
        => gameObject.activeSelf;
    //设置Collider的显示 激活状态
    #endregion
    #region 事件相关
    private void Hit(params object[] args)
    {
        ExcuteAllAction();
        ResetCombatIndex();
        if (_curType != E_Attack.NONE)
            _anim.ResetTrigger(_curType.ToString());
        SetActiveCollider(false);
        _isAttacking = false;
        ResetCurAttackType();
    }
    private void HitOver(params object[] args)
        => SetActive(true);
    private void SetCanFlip()
        => SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_CANFLIP);
    private void StopCanFlip()
        => SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_STOPFLIP);
    //无敌帧
    private void Domineering()
        => SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_INVINCIBLE);
    //取消无敌帧
    private void StopDomineering()
        => SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_STOPINVINCIBLE);
    #endregion
    #region 受伤特效显示相关  
    //检测受伤特效是否完毕
    //显示受伤特效
    protected virtual void ShowHitEffect(params object[] args)
    {
        var effectGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_SWORDHITPARRYEFFECT);
        effectGo.transform.position = transform.position;
        effectGo.transform.rotation = Quaternion.identity;
        effectGo.AddOrGet<AutoRecycleComponent>().Init(1f);
        _isHit = true;
    }
    #endregion
    #region 其他
    //添加攻击效果
    protected virtual void AttackEffect(AttackDetails attackDetail, Collider2D collider)
    {
        var bulleTime = attackDetail.bulletTime;
        var cameraShakeArgs = attackDetail.cameraShakeArgs;
        var shakeTime = cameraShakeArgs.cameraShakeTime;
        var shakeStrength = cameraShakeArgs.cameraShakeStrength;
        var shakeFrequency = cameraShakeArgs.cameraShakeFrequency;
        shakeStrength = (!_isHit) ? shakeStrength : shakeStrength * Consts.CHARACTER_PARRY_MULT;
        //如果子弹时间不为0
        if (bulleTime != 0f)
        {
            TimeScaleMgr.Instance.OpenSlowOutBulletTime(bulleTime, (!_isHit) ? bulleTime : bulleTime * Consts.CHARACTER_PARRY_MULT);
            VolumeMgr.Instance.OpenAttackEffect((!_isHit) ? bulleTime : bulleTime * Consts.CHARACTER_PARRY_MULT);
        }
        //如果相机振动时间不为0
        if (shakeTime != 0)
            CameraMgr.Instance.DoCameraNormalShake(shakeTime, shakeStrength, shakeFrequency);
        //产生攻击特效
        if (IsMeleeAttackType(attackDetail.attackType))
        {
            CreateMeleeAttackEffect(collider);
            if (_isHit)
                CreateMeleeAttackEffect(collider);
        }
    }
    protected virtual void CreateMeleeAttackEffect(Collider2D collider)
    {
        GameObject effectGo;
        if (collider.CompareTag(Tags.Player))
            effectGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_SWORDHITENEMY);
        //如果不是敌人 是子弹 可摧毁物体
        else
            effectGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_SWORDHITBULLET);
        effectGo.transform.position = collider.bounds.ClosestPoint(transform.position);
        effectGo.transform.rotation = Quaternion.identity;
        effectGo.AddOrGet<AutoRecycleComponent>().Init(1f);
    }
    //判断当前攻击是否是近战攻击
    protected bool IsMeleeAttackType(E_Attack attackType)
        => attackType.ToString().Contains(Consts.NAME_MELEETATTACK_TYPE);
    //判断当前攻击是否是远程攻击
    protected bool IsRangeAttackType(E_Attack attackType)
        => attackType.ToString().Contains(Consts.NAME_RANGEATTACK_TYPE);
    public int GetOwerFacingDir()
        => _owerTf.localEulerAngles.y == 0 ? 1 : -1;
    #endregion
    //攻击效果
}
