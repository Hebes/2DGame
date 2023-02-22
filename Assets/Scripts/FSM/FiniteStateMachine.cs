using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public IBaseState CurState { get; private set; }
    private Dictionary<E_CharacterState, IBaseState> _mapDic = new Dictionary<E_CharacterState, IBaseState>();
    public E_CharacterState CurStateName { get; private set; }
    public void EnterFirstState() => CurState.Enter();
    public void AddState(E_CharacterState newStateName,IBaseState newState)
    {
        if (CurState == null)
        {
            CurState = newState;
            CurStateName = newStateName;
        }
        if (!_mapDic.ContainsKey(newStateName))
            _mapDic[newStateName] = newState;
        else
            Debug.LogError($"名为:{newStateName}的状态被重复添加");
    }
    public void RemoveState(E_CharacterState newStateName)
    {
        if (CurStateName == newStateName)
        {
            Debug.LogError($"不能移除当前状态：{CurStateName}");
            return;
        }
        if (_mapDic.ContainsKey(newStateName))
            _mapDic.Remove(newStateName);
        else
            Debug.LogError($"移除的状态名称:{newStateName.ToString()}不存在");
    }
    public void ChangeState(E_CharacterState stateName)
    {
        if(CurState==null)
        {
            Debug.LogError($"未初始化状态");
            return;
        }
        if(!_mapDic.ContainsKey(stateName))
        {
            Debug.LogError($"要切换的状态:{stateName}不存在在于状态列表中");
            return;
        }    
        CurState.Exit();
        CurState = _mapDic[stateName];
        CurStateName = stateName;
        CurState.Enter();
    }
    public void ActionUpdate() => CurState.ActionUpdate();
    public void ReasonUdpate() => CurState.ReasonUpdate();
    public void PhysicsUpdate() => CurState.PhysicsUpdate();
    public Y GetState<Y>() where Y:BaseState
    {
        foreach (var value in _mapDic.Values)
        {
            if (value is Y)
                return value as Y;
        }
        Debug.LogError($"当前状态名：{typeof(Y)}不存在于状态机中");
        return default(Y);
    }    //得到你想要的状态
    public void AnimationEnterTrigger() => CurState.AnimatorEnterTrigger();
    public void AnimationFinishTrigger() => CurState.AnimatorFinishTrigger();

    public void GetTransitionStateToAllState<T,X>(Func<bool> func,E_CharacterState state) where T:MonoBehaviour where X:ScriptableObject
    {
        foreach (var st in _mapDic.Values)
            (st as FSMBaseState<T, X>).InsertStateInDir(func,state);
    }
    
}
