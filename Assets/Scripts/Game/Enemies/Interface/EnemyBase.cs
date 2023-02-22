using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EnemyBase<T> : MonoBehaviour where T : EnemyBaseData
{
    [SerializeField]
    protected T _data;
    public Core Core { get; private set; }
    protected FiniteStateMachine Fsm { get; private set; }
    public Animator Anm { get; private set; }
    public SubEventMgr SubEventMgr { get; set; }
    public SpriteRenderer Render { get; private set; }
    protected bool _canToHit = false;
    protected bool _canToDead = false;
    protected bool _canToRigidity = false;

    protected virtual void Awake()
    {
        Fsm = new FiniteStateMachine();
        InitComponent();
        InitState();
        AddEvent();
    }
    protected virtual void OnDestroy()
    {
        RemoveEvent();
    }
    protected virtual void Update()
    {
        Fsm.ActionUpdate();
        CompelToTargetState();
        Fsm.ReasonUdpate();
    }
    //强制进入目标状态
    protected virtual void CompelToTargetState()
    {
        if (_canToHit)
        {
            CompelChangeState(E_CharacterState.HIT);
            _canToHit = false;
        }
        if (_canToRigidity)
        {
            CompelChangeState(E_CharacterState.RIGIDITY);
            _canToRigidity = false;
        }
        if (_canToDead)
        {
            CompelChangeState(E_CharacterState.DEAD);
            _canToDead = false;
        }
    }
    protected virtual void FixedUpdate()
    {
        Fsm.PhysicsUpdate();
    }
    protected virtual void AddEvent()
    {
        SubEventMgr.AddEvent(E_EventName.CHARACTER_HIT, ChangeToHitState);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_RIGIBIDY, ChangeToRigidityState);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_DEAD, ChangeToDeadState);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_PARRY, Parry);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_INVINCIBLE, SetDomineering);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_STOPINVINCIBLE, StopDomineering);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_DOMINEERING, ShowHurtEffect);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_CREATEDEADSPRITE, CreateDeadSprite);
    }
    protected virtual void RemoveEvent()
    {
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_HIT, ChangeToHitState);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_RIGIBIDY, ChangeToRigidityState);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_DEAD, ChangeToDeadState);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_PARRY, Parry);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_INVINCIBLE, SetDomineering);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_STOPINVINCIBLE, StopDomineering);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_DOMINEERING, ShowHurtEffect);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_CREATEDEADSPRITE, CreateDeadSprite);
    }
    protected virtual void InitComponent()
    {
        Core = transform.Find("Core").GetComponent<Core>();
        Core.Init();
        Anm = GetComponent<Animator>();
        Render = GetComponent<SpriteRenderer>();
        SubEventMgr = gameObject.AddOrGet<SubEventMgr>();
    }
    protected abstract void InitState();
    protected virtual void AnimatorEnterTrigger() => Fsm.AnimationEnterTrigger();
    protected virtual void AnimatorFinishTrigger() => Fsm.AnimationFinishTrigger();
    protected virtual void SetPos(Vector3 pos) => transform.position = pos;
    //切换状态至受伤状态
    protected virtual void ChangeToHitState(params object[] args)
    {
        _canToHit = true;
        //受伤的时候不能攻击
        Core.Get<CombatComponent>().SetActive(false);
    }
    //切换状态至Dead状态
    protected virtual void ChangeToDeadState(params object[] args)
    {
        _canToDead = true;
        //进入死亡状态后 即不能在被攻击和攻击了
        Core.Get<CombatComponent>().SetActive(false);
        Core.Get<EnemyBehaviorComponent>().SetActive(false);
        CreateCoin();
        AddHeroMagicPower();
    }
    protected virtual void ChangeToRigidityState(params object[] args)
    {
        _canToRigidity = true;
    }
    //真正完成状态切换的操作
    protected virtual void CompelChangeState(E_CharacterState state)
    {
        if (Fsm.CurStateName != state)
            Fsm.ChangeState(state);
    }
    //弹反
    protected virtual void Parry(params object[] args)
    {
        var bullet = args[0] as IBulletBehavior;
        var selfGroup = Core.Get<EnemyBehaviorComponent>().GetGroup();
        var hostility = Core.Get<CombatComponent>().GetHostileGroup();
        bullet.Parry(selfGroup, hostility);
    }
    //无敌帧
    protected virtual void SetDomineering(params object[] args)
     => Core.Get<EnemyBehaviorComponent>().SetDomineering(true);
    //取消无敌帧
    protected virtual void StopDomineering(params object[] args)
     => Core.Get<EnemyBehaviorComponent>().SetDomineering(false);
    //显示受伤特效
    private void ShowHurtEffect(params object[] args)
    {
        //todo添加受伤特效

    }
    //产生遗骸
    protected void CreateDeadSprite(params object[] args)
    {
        var deadGo = new GameObject(transform.name + Consts.NAME_DESTRUCTIBLEITEM_DEAD);
        var render = deadGo.AddComponent<SpriteRenderer>();
        render.sprite = Render.sprite;
        render.sortingLayerID = Render.sortingLayerID;
        render.sortingOrder = Render.sortingOrder;
        render.color = Render.color;
        render.material = Render.material;
        deadGo.transform.position = transform.position;
        deadGo.transform.rotation = transform.rotation;
    }
    public async void CreateCoin()
    {
        var pos = transform.position+(Vector3)_data.deadCoinCreateOffset;
        int num = _data.deadCoin;
        for (int i = 0; i < num; i++)
        {
            var coinGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_COIN);
            coinGo.transform.position = pos;
            await Task.Delay(TimeSpan.FromSeconds(0.05f));
        }
    }
    private void AddHeroMagicPower()
    {
        float value = _data.deadMagicPower;
        EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_ADDMAGICPOWER, value);
    }
}
