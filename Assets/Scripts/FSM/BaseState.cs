using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IBaseState
{
    public bool AnimFinish { get; protected set; }
    protected FiniteStateMachine _fsm;
    protected string _animName;
    protected float _enterTime;
    public BaseState(FiniteStateMachine fsm, string animBoolName)
    {
        _fsm = fsm;
        _animName = animBoolName;
    }
    public virtual void ActionUpdate()
    {

    }

    public virtual void Check()
    {

    }
    public virtual void Enter()
    {
        _enterTime = Time.time;
        AnimFinish = false;
        Check();
    }
    public virtual void Exit()
    {

    }
    public virtual void PhysicsUpdate()
    {
        Check();
    }
    public virtual void ReasonUpdate()
    {

    }

    public virtual void AnimatorEnterTrigger()
    {
    }

    public virtual void AnimatorFinishTrigger()
    {
        AnimFinish = true;
    }
}
public abstract class FSMBaseState<T, X> : BaseState where T : MonoBehaviour where X : ScriptableObject
{
    protected T _ower;
    protected X _data;
    protected Animator _anim;
    protected Core _core;
    protected Dictionary<Func<bool>, E_CharacterState> _targetStateDic;
    protected Dictionary<E_CharacterState, Action> _changeStateAfterAction;//状态切换后执行的事件
    protected Dictionary<E_CharacterState, Action> _changeStateBeforeAction;//状态切换前执行的事件
    private RgMoveComponent _move;
    private ColliderCheckComponent _collideCheck;
    private CombatComponent _combat;
    //todoBehavior对应的属性
    protected RgMoveComponent Move
    {
        get
        {
            if (_move == null)
                _move = _core.Get<RgMoveComponent>();
            return _move;
        }
    }
    protected ColliderCheckComponent ColliderCheck
    {
        get
        {
            if (_collideCheck == null)
                _collideCheck = _core.Get<ColliderCheckComponent>();
            return _collideCheck;
        }
    }
    protected CombatComponent Combat
    {
        get
        {
            if (_combat == null)
                _combat = _core.Get<CombatComponent>();
            return _combat;
        }
    }
    public FSMBaseState(FiniteStateMachine fsm, string animName, T ower, X data) : base(fsm, animName)
    {
        _fsm = fsm;
        _targetStateDic = new Dictionary<Func<bool>, E_CharacterState>();
        _changeStateBeforeAction=new Dictionary<E_CharacterState, Action>();
        _changeStateAfterAction = new Dictionary<E_CharacterState, Action>();
        _ower = ower;
        _data = data;
        FindAnimAndCore();
        if (_anim == null)
            Debug.LogError($"当前角色:{_ower.name}身上没有动画状态机");
        if (_core == null)
            Debug.LogError($"当前角色:{_ower.name}身上没有Core组件");
    }
    public override void Enter()
    {
        PlayeAnim();
        base.Enter();
    }
    protected virtual void PlayeAnim()
    {
        _anim.Play(_animName);
    }
    public void AddTargetState(Func<bool> func, E_CharacterState state, Action actionAfter = null,Action actionBefore=null)
    {
        //todo 可能要做一些修改
        if (!_targetStateDic.ContainsKey(func))
        {
            _targetStateDic[func] = state;
            if (!_changeStateAfterAction.ContainsKey(state) && actionAfter != null)
                _changeStateAfterAction[state] = actionAfter;
            if (!_changeStateBeforeAction.ContainsKey(state) && actionBefore != null)
                _changeStateBeforeAction[state] = actionBefore;
        }
        else
            Debug.LogError($"当前转换条件已经添加过了,一个转换条件只能对应一种状态");
    }
    //如果满足转换条件则转换状态
    public void CheckIfCanChangsState()
    {
        foreach (var func in _targetStateDic.Keys)
        {
            if (func())
            {
                var value = _targetStateDic[func];
                if (_changeStateBeforeAction.ContainsKey(value))
                    _changeStateBeforeAction[value]?.Invoke();
                _fsm.ChangeState(value);//Enter函数 和 Exit函数执行之后 才会执行回调函数
                if (_changeStateAfterAction.ContainsKey(value))
                    _changeStateAfterAction[value]?.Invoke();
                break;
            }
        }
    }
    public override void ReasonUpdate()
    {       
        base.ReasonUpdate();
        CheckIfCanChangsState();
    }
    public abstract void FindAnimAndCore();
    //判断是否有该装状态
    public bool HasThisTargetState(E_CharacterState state)
    {
        foreach (var value in _targetStateDic.Values)
            if (value == state)
                return true;
        return false;
    }
    //设置插入的状态为当前状态优先进入的状态
    public void InsertStateInDir(Func<bool> func, E_CharacterState state, Action action = null)
    {
        var tempDic = new Dictionary<Func<bool>, E_CharacterState>();
        tempDic.Add(func, state);
        if (!_changeStateAfterAction.ContainsKey(state) && action != null)
            _changeStateAfterAction[state] = action;
        foreach (var kv in _targetStateDic)
            tempDic.Add(kv.Key, kv.Value);
        _targetStateDic = tempDic;
        tempDic = null;
    }
}




