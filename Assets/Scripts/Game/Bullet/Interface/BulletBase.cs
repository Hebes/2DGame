using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase<T> : MonoBehaviour, IBulletBase where T : BulletBaseData
{
    [SerializeField]
    protected T _data;
    protected Core Core { get; private set; }
    protected SubEventMgr SubEventMgr { get; private set; }
    protected Animator Anim { get; private set; }
    protected SpriteRenderer SpriteRender { get; private set; }
    protected RgMoveComponent _move;
    protected Vector2 _curDir;
    protected bool _isOver;
    protected float _startTime;
    protected float _startRotateTime;
    protected bool _canRotate;
    protected bool _isAdd;
    protected float _deadAnimLength;//死亡动画的长度 来判断该Bullet是否有死亡动画
    protected virtual void Awake()
    {
        InitComponent();
        AddEvent();
        _deadAnimLength = GetAnimLength(Consts.CHARACTER_ANM_DEAD);
    }
    protected virtual void OnDestroy()
    {
        RemoveEvent();
    }
    protected virtual void OnEnable()
    {
        //清空重力的影响
        _move.SetGravityScale();
        transform.rotation = Quaternion.identity;
    }
    protected virtual void Update()
    {
        if ( !gameObject.activeSelf)
            return;
        if (!_isOver)
            _move.SetVelocity(_data.movementVelocity, _curDir);
        else
            _move.SetXVelocity(0);
        CheckInOverMaxFlyTime();
    }
    //如果撞到墙
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        KnockWallOrGround();
        var pos = collision.collider.bounds.ClosestPoint(transform.position);
        Core.Get<BulletCombatComponent>().CreateBulletBounceEffect(pos);
    }
    public virtual void Init(Vector2 dir, E_Group selfGroup, HashSet<E_Group> hostilityGroup, Vector3 startPos/*, bool isFlip = false*/)
    {
        _isOver = false;
        _curDir = dir;
        _startTime = Time.time;
        Core.Get<BulletBehaviorComponent>().SetGroup(selfGroup);
        Core.Get<BulletBehaviorComponent>().RecoverHealth();
        Core.Get<BulletCombatComponent>().SetActive(true);
        Core.Get<BulletCombatComponent>().SetHostileGroupHash(hostilityGroup);
        SetPos(startPos);
        transform.right = dir;
        PlayReadyAnim();
    }
    protected void AddEvent()
    {
        SubEventMgr.AddEvent(E_EventName.BULLET_REVERSEDIR, ReverseCurDir);
        SubEventMgr.AddEvent(E_EventName.BULLET_HIT, Hurt);
        SubEventMgr.AddEvent(E_EventName.BULLET_KNOCK, KnockWallOrGround);
    }
    protected void RemoveEvent()
    {
        SubEventMgr.RemoveEvent(E_EventName.BULLET_REVERSEDIR, ReverseCurDir);
        SubEventMgr.RemoveEvent(E_EventName.BULLET_HIT, Hurt);
        SubEventMgr.RemoveEvent(E_EventName.BULLET_KNOCK, KnockWallOrGround);
    }
    //播放预备动画
    private void PlayReadyAnim()
    {
        var readyAnimLength = GetAnimLength(Consts.CHARACTER_ANM_READY);
        if (readyAnimLength > 0)
            Anim.Play(Consts.CHARACTER_ANM_READY);
    }
    //得到动画片段你的长度
    private float GetAnimLength(string animName)
    {
        //让首字母大写
        animName = animName.Substring(0,1).ToUpper()+animName.Substring(1);
        var name = this.name.Replace("(Clone)", "") + $"_{animName}";
        return Anim.GetAnmLength(name);
    }
    protected virtual void InitComponent()
    {
        Core = Get<Core>("Core");
        Core.Init();
        SubEventMgr = transform.AddOrGet<SubEventMgr>();
        Anim = Get<Animator>();
        SpriteRender = Get<SpriteRenderer>();
        _move = Core.Get<RgMoveComponent>();
        //初始化伤害
        Core.Get<BulletCombatComponent>().SetDamage(_data.damageValue);
    }
    protected X Get<X>() where X : Component
    {
        X component = GetComponent<X>();
        if (component == null)
            Debug.LogError($"Bullet物体:{name}身上不存在该组件:{typeof(X)}");
        return component;
    }
    protected X Get<X>(string path) where X : Component
    {
        X component = transform.Find(path).GetComponent<X>();
        if (component == null)
            Debug.LogError($"当前Bomb物体:{name}的:{path}路径下为未找到该组件:{typeof(X)}");
        return component;
    }
    protected void CheckInOverMaxFlyTime()
    {
        if (!_isOver && Time.time >= _startTime + _data.maxFlyTime)
            StopBullet();
    }
    //停止当前子弹的正常运动
    protected virtual void StopBullet()
    {
        _isOver = true;
        _move.ResetGravityScale();//还原重力的大小的影响
        _move.SetXVelocity(0);
        AddTorque(_data.torqueValue);
        //添加自动回收组件
        Core.Get<BulletCombatComponent>().SetActive(false);
        RecycleInConstTime();
    }
    protected virtual void RecycleInConstTime()
    {
        if (_deadAnimLength != 0f)
        {
            Anim.Play(Consts.CHARACTER_ANM_DEAD);
            gameObject.AddOrGet<AutoRecycleComponent>().Init(_deadAnimLength);
        }
        else
            gameObject.AddOrGet<AutoRecycleComponent>().Init(_data.recycleTime);
    }
    protected virtual void DestroyInConstTime()
    {
        if (_deadAnimLength != 0f)
        {
            Anim.Play(Consts.CHARACTER_ANM_DEAD);
            gameObject.AddOrGet<AutoDestroyComponent>().Init(_deadAnimLength);
        }
        else
            gameObject.AddOrGet<AutoDestroyComponent>().Init(_data.recycleTime);
    }
    public void SetPos(Vector3 pos)
        => transform.position = pos;
    //添加扭矩力
    protected virtual void AddTorque(float value)
    {
        if (_move.FacingDir == -1)
            value = -value;
        _move.RB.AddTorque(value);
    }
    private void KnockWallOrGround(params object[] args)
    {
        if (_isOver)
            return;
        StopBullet();
    }
    //反向
    private void ReverseCurDir(params object[] args)
    {
        _curDir = _curDir.Reverse();
        SetBulletDir(_curDir);
        var selfGroup = (E_Group)args[0];
        var hostilityGroup = (HashSet<E_Group>)args[1];
        Core.Get<BulletBehaviorComponent>().SetGroup(selfGroup);
        Core.Get<BulletCombatComponent>().SetHostileGroupHash(hostilityGroup);
        _startTime = Time.time;
    }
    //受伤
    private void Hurt(params object[] args)
    {
        //todo添加受伤特效
    }
    //设置子弹方向
    private void SetBulletDir(Vector2 dir)
        => transform.right = dir;
}
