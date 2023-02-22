using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AbilityBaseState
{
    protected bool _wallFront;
    protected bool _isGrounded;
    protected bool _startDash;
    protected Vector3 _lastShadowPos;
    private string _audioClipName = "hero_dash";
    private bool _readyDash=false;
    public DashState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Check()
    {
        base.Check();
        _wallFront = ColliderCheck.WallFront;
        _isGrounded = ColliderCheck.Ground;
    }
    public override void Enter()
    {
        base.Enter();
        Move.SetVelocityZero();
        _startDash = false;
        CreateOneDash();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_DASH, true);
        AudioMgr.Instance.PlayOnce(_audioClipName);
        SetReadyDash(false);
    }
    public override void Exit()
    {
        base.Exit();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_DASH, false);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        if(!_startDash) return;
        Move.SetYVelocity(0);
        Move.SetXVelocityInFacing(_data.dashVelocity,true);
        CheckDashIsOver();
        CreateDashShadow();
    }
    public override E_CharacterState TargetState()
        => _isGrounded ? E_CharacterState.IDLE : E_CharacterState.INAIR;
    protected void CheckDashIsOver()
    {
        if (Time.time >= _enterTime + _data.dashTime||_wallFront)
            SetAbilityDown();
    }
    //检测冲刺时间是否完毕
    public bool CheckCanDash()
        => Time.time >= _enterTime + _data.dashCoolTime&&_readyDash;
    public void SetReadyDash(bool value)
        => _readyDash = value;
    public override void AnimatorEnterTrigger()
    {
        base.AnimatorEnterTrigger();
        _startDash = true;
        _anim.Play(Consts.CHARACTER_ANM_DASHLOOP);
    }
    //生成残影
    private void CreateDashShadow()
    {
        if (Vector3.Distance(_lastShadowPos, _ower.transform.position) > _data.dashShadowCreateDis)
            CreateOneDash();
    }
    private void CreateOneDash()
    {
        PoolManager.Instance.GetFromPool(_data.dashShadowPrefab);
        _lastShadowPos = _ower.transform.position;
    }
}
