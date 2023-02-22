/****************************************************
    文件：BombBase.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/5 11:43:4
	功能：炸弹基类
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombBase<T> : MonoBehaviour, IBombBase where T : BombBaseData
{
    [SerializeField]
    protected T _data;
    protected Core Core { get; private set; }
    protected SubEventMgr SubEventMgr { get; private set; }
    protected Animator Anim { get; private set; }
    protected SpriteRenderer SpriteRender { get; private set; }
    protected Rigidbody2D Rigid { get; private set; }
    protected Vector2 _curDir;//当前方向
    protected bool _isExplosion = false;//是否开始爆炸
    protected virtual void Awake()
    {
        InitComponent();
        AddEvent();
        AddForce();
        AddTorqueForece();
    }
    protected virtual void OnEnable()
    {
        _isExplosion = false;
    }
    protected virtual void OnDestroy()
    {
        RemoveEvent();
    }
    protected virtual void Update()
    {
        if (!gameObject.activeSelf)
            return;
    }
    public virtual void Init(Vector3 pos,Vector2 dir, E_Group selfGroup, HashSet<E_Group> hostilityGroup)
    {
        SetPos(pos);
        SetDir(dir);
        //默认状态下炸弹不能被攻击
        Core.Get<BombBehaviorComponent>().SetActive(false);
        Core.Get<BombCombatComponent>().SetActive(false);
        Core.Get<BombBehaviorComponent>().RecoverHealth();
        Core.Get<BombBehaviorComponent>().SetGroup(selfGroup);
        Core.Get<BombCombatComponent>().SetHostileGroupHash(hostilityGroup);
    }
    protected virtual void InitComponent()
    {
        Core = Get<Core>("Core");
        Core.Init();
        Core.Get<BombCombatComponent>().SetDamage(_data.damageValue);
        Anim = Get<Animator>();
        SpriteRender = Get<SpriteRenderer>();
        Rigid = Get<Rigidbody2D>();
        SubEventMgr = transform.AddOrGet<SubEventMgr>();
    }
    public void InitStartDir(Vector2 startDir)
        => _curDir = startDir.normalized;
    //泛型方法 可能会导致在Animator事件面板报错
    protected X Get<X>() where X : Component
    {
        X component = GetComponent<X>();
        if (component == null)
            Debug.LogError($"当前Bomb物体:{name}身上不存在该组件:{typeof(X)}");
        return component;
    }
    protected X Get<X>(string path) where X : Component
    {
        X component = transform.Find(path).GetComponent<X>();
        if (component == null)
            Debug.LogError($"当前Bomb物体:{name}的:{path}路径下为未找到该组件:{typeof(X)}");
        return component;
    }
    protected virtual void AddEvent()
    {
        SubEventMgr.AddEvent(E_EventName.BOMB_EXLPOSTION, BeforeExplosion);
    }
    protected virtual void RemoveEvent()
    {
        SubEventMgr.RemoveEvent(E_EventName.BOMB_EXLPOSTION, BeforeExplosion);
    }
    protected virtual void AddForce()
        => Rigid.AddRelativeForce(_curDir * _data.startForce);
    protected virtual void AddTorqueForece()
        => Rigid.AddTorque(_data.startTorqueForece);
    //提前爆炸的事件
    private void BeforeExplosion(params object[] args)
    {
        ExplostionStart();
        Anim.Play(Consts.CHARACTER_ANM_EXPLOSITION);
    }
    //爆炸开始
    protected virtual void ExplostionStart()
    {
        if (_isExplosion)
            return;
        _isExplosion = true;
        Core.Get<BombBehaviorComponent>().SetActive(false);
        Core.Get<BombCombatComponent>().SetActive(true);
    }
    //爆炸结束
    protected virtual void ExplostionOver()
    {
        Core.Get<BombCombatComponent>().SetActive(false);
        PoolManager.Instance.PushPool(gameObject);
    }
    //设置位置和方向
    protected void SetPos(Vector3 pos)
        => transform.position = pos;
    protected void SetDir(Vector2 dir)
        => transform.right = dir;
}