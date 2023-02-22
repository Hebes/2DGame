using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HeroMan : MonoBehaviour
{
    [SerializeField]
    private HeroManData _data;
    private FiniteStateMachine _fsm;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _render;
    private ShowHurtMaterialComponent _showHurtEffect;
    public Animator Anim { get; private set; }
    public Core Core { get; private set; }
    public PlayerInputHandle InputHandle { get; set; }
    public SubEventMgr SubEventMgr { get; private set; }
    private GameObject AttractCoinEffect;
    private GameObject ImproveEffect;
    private GameObject RecoverAllEffect;
    private GameObject DashEffect;
    private GameObject WalkEffect;
    private GameObject ShieldGo;
    private Transform JumpAndLandCreateTf;

    protected bool _canToHit = false;
    protected bool _canToDead = false;
    protected bool _isResetPos = false;
    protected bool _canToGetItem = false;


    //气血丹相关属性 这里偷个懒...
    private float _improveRecoverTime = 12f;
    private float _improveRecoverRatio = 2f;
    //可以吸收金币的状态名
    private HashSet<E_CharacterState> _canAttractCoinState = new HashSet<E_CharacterState>()
    {
        E_CharacterState.IDLE,
        E_CharacterState.MOVE,
        E_CharacterState.INAIR,
        E_CharacterState.CROUCHIDLE,
        E_CharacterState.CROUCHMOVE,
    };
    private float _attrackTimer;
    private void Awake()
    {
        _fsm = new FiniteStateMachine();
        InitComponent();
        InitState();
        AddEvent();
    }
    private void OnDestroy()
    {
        RemoveEvent();
    }
    private void AddEvent()
    {
        SubEventMgr.AddEvent(E_EventName.CHARACTER_HIT, ChangeToHitState);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_DEAD, ChangeToDeadState);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_CANFLIP, SetCanFlip);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_STOPFLIP, StopCanFlip);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_PARRY, Parry);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_INVINCIBLE, SetDomineering);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_STOPINVINCIBLE, StopDomineering);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_DOMINEERING, ShowHurtEffect);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_CHECKCANRECOVER, CheckNeedRecoverHpByLightValue);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_DASH, PlayerDashStartOrOver);
        SubEventMgr.AddEvent(E_EventName.CHARACTER_SETSHIELD, SetShieldEffevtAvtive);

        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_GETITEM, ChangeToGetItemState);
        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_REST, PlayerRest);
        EventMgr.Instance.AddEvent(E_EventName.USEITEM_RECOOVERALLHP, UseItem_RecoverAllHp);
        EventMgr.Instance.AddEvent(E_EventName.USEITEM_IMPROVERECOVER_TEMPORARY, UseItem_ImporveRecover);
    }
    private void RemoveEvent()
    {
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_HIT, ChangeToHitState);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_DEAD, ChangeToDeadState);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_CANFLIP, SetCanFlip);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_STOPFLIP, StopCanFlip);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_PARRY, Parry);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_INVINCIBLE, SetDomineering);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_STOPINVINCIBLE, StopDomineering);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_DOMINEERING, ShowHurtEffect);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_CHECKCANRECOVER, CheckNeedRecoverHpByLightValue);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_DASH, PlayerDashStartOrOver);
        SubEventMgr.RemoveEvent(E_EventName.CHARACTER_SETSHIELD, SetShieldEffevtAvtive);

        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_GETITEM, ChangeToGetItemState);
        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_REST, PlayerRest);
        EventMgr.Instance.RemoveEvent(E_EventName.USEITEM_RECOOVERALLHP, UseItem_RecoverAllHp);
        EventMgr.Instance.RemoveEvent(E_EventName.USEITEM_IMPROVERECOVER_TEMPORARY, UseItem_ImporveRecover);
    }
    private void Update()
    {
        _fsm.ActionUpdate();
        if (_canToGetItem)
        {
            CompelChangeState(E_CharacterState.GETITEM);
            _canToGetItem = false;
        }
        if (_canToHit)
        {
            if (_isResetPos)
            {
                _fsm.GetState<HitState>().SetIsResetPos();
            }
            CompelChangeState(E_CharacterState.HIT);
            _canToHit = false;
            _isResetPos = false;
        }
        if (_canToDead)
        {
            CompelChangeState(E_CharacterState.DEAD);
            _canToDead = false;
        }
        _fsm.ReasonUdpate();
        CheckCanAttrackCoin();
    }
    private void FixedUpdate()
    {
        _fsm.PhysicsUpdate();
    }
    public void InitComponent()
    {
        ShieldGo = transform.Find("Shield").gameObject;
        AttractCoinEffect = transform.Find("AttractCoinEffect").gameObject;
        ImproveEffect = transform.Find("ImproveEffect").gameObject;
        RecoverAllEffect = transform.Find("RecoverAllEffect").gameObject;
        DashEffect = transform.Find("DashEffect").gameObject;
        WalkEffect = transform.Find("WalkEffect").gameObject;
        JumpAndLandCreateTf = transform.Find("JumpAndLandEffectCreatePos").transform;
        SubEventMgr = gameObject.AddOrGet<SubEventMgr>();
        Core = transform.Find("Core").GetComponent<Core>();
        InputHandle = GetComponent<PlayerInputHandle>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _render = GetComponent<SpriteRenderer>();
        _showHurtEffect = gameObject.AddOrGet<ShowHurtMaterialComponent>();
        _showHurtEffect.Init(_render, Consts.CHARACTER_SHOWHURTEFFECT_TIME);
        Anim = GetComponent<Animator>();
        Core.Init();
        if (Core == null)
            Debug.LogError($"当前物体:{name}身上没有Core组件");
        SetRecoverAllEffectActive(false);
        SetImproveEffectActive(false);
        SetDashEffectActive(false);
        SetWalkEffectActive(false);
        SetShieldEffevtAvtive(false);
    }
    public void InitState()
    {
        _fsm.AddState(E_CharacterState.IDLE, new IdleState(_fsm, Consts.CHARACTER_ANM_IDLE, this, _data));
        _fsm.AddState(E_CharacterState.MOVE, new MoveState(_fsm, Consts.CHARACTER_ANM_MOVE, this, _data));
        _fsm.AddState(E_CharacterState.JUMP, new JumpState(_fsm, Consts.CHARACTER_ANM_JUMP, this, _data));
        _fsm.AddState(E_CharacterState.INAIR, new InAirState(_fsm, Consts.CHARACTER_ANM_INAIR, this, _data));
        _fsm.AddState(E_CharacterState.LAND, new LandState(_fsm, Consts.CHARACTER_ANM_LAND, this, _data));
        _fsm.AddState(E_CharacterState.CROUCHIDLE, new CrouchIdleState(_fsm, Consts.CHARACTER_ANM_CROUCH_IDLE, this, _data));
        _fsm.AddState(E_CharacterState.CROUCHMOVE, new CrouchMoveState(_fsm, Consts.CHARACTER_ANM_CROUCH_MOVE, this, _data));
        _fsm.AddState(E_CharacterState.SLIDE, new SlideState(_fsm, Consts.CHARACTER_ANM_SLIDE, this, _data));
        _fsm.AddState(E_CharacterState.ROLL, new RollState(_fsm, Consts.CHARACTER_ANM_ROLL, this, _data));
        _fsm.AddState(E_CharacterState.WALLSLIDE, new WallSlideState(_fsm, Consts.CHARACTER_ANM_WALLSLIDE, this, _data));
        _fsm.AddState(E_CharacterState.WALLGRAB, new WallGrabState(_fsm, Consts.CHARACTER_ANM_WALLGRAB, this, _data));
        _fsm.AddState(E_CharacterState.WALLCLIMB, new WallClimbState(_fsm, Consts.CHARACTER_ANM_WALLCLIMB, this, _data));
        _fsm.AddState(E_CharacterState.LEDGECLIMB, new LedgeClimbState(_fsm, Consts.CHARACTER_ANM_LEDGE_START, this, _data));
        _fsm.AddState(E_CharacterState.LEDGEJUMP, new LedgeJumpState(_fsm, Consts.CHARACTER_ANM_LEDGE_JUMP, this, _data));
        _fsm.AddState(E_CharacterState.WALLJUMP, new WallJumpState(_fsm, Consts.CHARACTER_ANM_WALLJUMP, this, _data));
        _fsm.AddState(E_CharacterState.DASH, new DashState(_fsm, Consts.CHARACTER_ANM_DASH, this, _data));
        _fsm.AddState(E_CharacterState.MELEEATTACK, new MeleeAttackState(_fsm, Consts.CHARACTER_ANM_MELEEATTACK, this, _data));
        _fsm.AddState(E_CharacterState.MELEEAIRATTACK, new MeleeAirAttackState(_fsm, Consts.CHARACTER_ANM_AIRMELEEATTACK, this, _data));
        _fsm.AddState(E_CharacterState.MELEEAIRATTACK_LOOP, new MeleeAirAttackLoop(_fsm, Consts.CHARACTER_ANM_AIRMELEEATTACK_LOOP, this, _data));
        _fsm.AddState(E_CharacterState.RANGEATTACK, new RangeAttackState(_fsm, Consts.CHARACTER_ANIM_RANGEATTACK, this, _data));
        _fsm.AddState(E_CharacterState.RANGEAIRATTACK, new RangeAirAttackState(_fsm, Consts.CHARACTER_ANOM_RANGEAIRATTACK, this, _data));
        _fsm.AddState(E_CharacterState.HIT, new HitState(_fsm, Consts.CHARACTER_ANM_HIT, this, _data));
        _fsm.AddState(E_CharacterState.DEAD, new DeadState(_fsm, Consts.CHARACTER_ANM_DEAD, this, _data));
        _fsm.AddState(E_CharacterState.GETITEM, new GetItemState(_fsm, Consts.CHARACTER_ANM_GETITEM, this, _data));
        _fsm.EnterFirstState();
    }
    public void AnimationEnterTrigger() => _fsm.AnimationEnterTrigger();
    public void AnimatonFinishTrigger() => _fsm.AnimationFinishTrigger();
    private void SetCanFlip(params object[] args) => Core.Get<RgMoveComponent>().CanFlip = true;
    private void StopCanFlip(params object[] args) => Core.Get<RgMoveComponent>().CanFlip = false;
    //改变碰撞体大小
    public void ChangeHeroColliderHeight(float height)
    {
        var size = _boxCollider.size;
        var offset = _boxCollider.offset;
        var value = (height - size.y) / 2;
        _boxCollider.offset = new Vector2(offset.x, offset.y + value);
        size.y = height;
        _boxCollider.size = size;
    }
    public void SetPosition(Vector3 pos) => transform.position = pos;
    //切换状态至受伤状态
    private void ChangeToHitState(params object[] args)
    {
        ShowHurtEffect();
        _canToHit = true;
        Core.Get<HeroCombatComponent>().SetActive(false);
        VolumeMgr.Instance.OpenHitEffect(Consts.CHARACTER_SHOWHURTEFFECT_TIME * 2);
        //如果碰到尖刺
        if (args.Length > 0)
        {
            var isResetPos = (bool)args[0];
            _isResetPos = isResetPos;
        }
    }
    //切换状态至Dead状态
    private void ChangeToDeadState(params object[] args)
    {
        ShowHurtEffect();
        _canToDead = true;
        //进入死亡状态后 即不能在被攻击和攻击了
        Core.Get<HeroCombatComponent>().SetActive(false);
        Core.Get<HeroBehaviorComponent>().SetActive(false);
    }
    //切换状态至GetItemState
    private void ChangeToGetItemState(params object[] args)
        => _canToGetItem = true;
    private void Parry(params object[] args)
    {
        var bullet = args[0] as IBulletBehavior;

        var selfGroup = Core.Get<HeroBehaviorComponent>().GetGroup();
        var hostility = Core.Get<HeroCombatComponent>().GetHostileGroup();
        bullet.Parry(selfGroup, hostility);
    }
    //显示受伤特效
    private void ShowHurtEffect(params object[] args)
        => _showHurtEffect.ShowHitEffect();
    private void SetDomineering()
        => Core.Get<HeroBehaviorComponent>().SetDomineering(true);
    private void StopDomineering()
        => Core.Get<HeroBehaviorComponent>().SetDomineering(false);
    private void SetDomineering(params object[] args)
        => Core.Get<HeroBehaviorComponent>().SetDomineering(true);
    private void StopDomineering(params object[] args)
        => Core.Get<HeroBehaviorComponent>().SetDomineering(false);
    private void PlayerRest(params object[] args)
    {
        Core.Get<HeroBehaviorComponent>().RecoverHp(-1, true);
        Core.Get<HeroCombatComponent>().RecoverMagicPower(-1, true);
    }
    private void UseItem_RecoverAllHp(params object[] args)
    {
        Core.Get<HeroBehaviorComponent>().RecoverHp(0, true);
        ShowRecoverHpEffect();
    }
    private async void ShowRecoverHpEffect()
    {
        if (RecoverAllEffect.activeSelf)
            return;
        SetRecoverAllEffectActive(true);
        await Task.Delay(TimeSpan.FromSeconds(1f));
        SetRecoverAllEffectActive(false);
    }
    private void UseItem_ImporveRecover(params object[] args)
    {
        bool value = (bool)args[0];
        if (value)
        {
            SetImproveEffectActive(true);
            Core.Get<HeroCombatComponent>().ImproveRecoverHp(_improveRecoverRatio, _improveRecoverTime);
        }
        else
            SetImproveEffectActive(false);
    }
    private void PlayerDashStartOrOver(params object[] args)
    {
        bool value = (bool)args[0];
        SetDashEffectActive(value);
    }
    //检测是否需要通过光源值来回复生命值
    private void CheckNeedRecoverHpByLightValue(params object[] args)
    {
        //返回值为百分比返回值
        if (Core.Get<HeroBehaviorComponent>().GetCurHealth() < 100 && Core.Get<HeroCombatComponent>().CheckCanRecoverHp())
        {
            Core.Get<HeroBehaviorComponent>().RecoverHp(1);
            ShowRecoverHpEffect();
        }
    }
    private void CompelChangeState(E_CharacterState state)
    {
        if (_fsm.CurStateName != state)
            _fsm.ChangeState(state);
    }
    public void ResetPos()
    {
        var pos = GamePosResetUtil.Instance.GetResetPos(transform.position);
        if (pos != Vector3.zero)
        {
            SetPos(pos);
            UIManager.Instance.ShowPanel(Paths.PREFAB_UIPANEL_GAMEBLACKPANEL);
        }
    }
    private void SetPos(Vector3 pos)
        => transform.position = pos;
    //检测是否可以吸收金币
    private void CheckCanAttrackCoin()
    {
        if (InputHandle.AttractInput && _canAttractCoinState.Contains(_fsm.CurStateName))
        {
            if (_attrackTimer < _data.attrackCoinMinPressTime)
                _attrackTimer += Time.deltaTime;
            else
            {
                SetAttackCoinEffectActive(true);
                EventMgr.Instance.ExecuteEvent(E_EventName.CHARACTER_ATTRACTCOIN, _data.attractCoinDis);
            }
        }
        else
        {
            _attrackTimer = 0;
            SetAttackCoinEffectActive(false);
        }
    }
    private void SetAttackCoinEffectActive(bool value)
    {
        if (AttractCoinEffect.activeSelf != value)
            AttractCoinEffect.gameObject.SetActive(value);
    }
    private void SetImproveEffectActive(bool value)
        => ImproveEffect.SetActive(value);
    private void SetRecoverAllEffectActive(bool value)
        => RecoverAllEffect.SetActive(value);
    private void SetDashEffectActive(bool value)
        => DashEffect.SetActive(value);
    public void SetWalkEffectActive(bool value)
        => WalkEffect.SetActive(value);
    private void SetShieldEffevtAvtive(params object[] args)
    {
        var value = (bool)args[0];
        ShieldGo.transform.DOKill();
        float animTime = 0.2f;
        if (value)
        {
            ShieldGo.SetActive(true);
            ShieldGo.transform.localScale = Vector3.zero;
            ShieldGo.transform.DOScale(Vector3.one * 0.4f, animTime)
                .SetEase(Ease.Linear)
                .SetUpdate(false);
        }
        else
        {
            ShieldGo.transform.DOScale(Vector3.zero, animTime)
             .SetEase(Ease.Linear)
             .SetUpdate(false)
             .OnComplete(() => ShieldGo.SetActive(false));
        }
    }
    //得到跳跃和落地特效产生的位置
    private Vector3 GetJumpAndLandEffectCreatePos()
        => JumpAndLandCreateTf.position;
    //产生跳跃特效
    public void CreateJumpEffect()
    {
        var effectGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_HEROJUMP);
        effectGo.transform.position = GetJumpAndLandEffectCreatePos();
        effectGo.AddOrGet<AutoRecycleComponent>().Init(1f);
    }
    //产生落地特效
    public void CreateLandEffect()
    {
        var effectGo = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_HEROLAND);
        effectGo.transform.position = GetJumpAndLandEffectCreatePos();
        effectGo.AddOrGet<AutoRecycleComponent>().Init(1f);
    }
}
