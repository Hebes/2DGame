using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbilityBaseState
{
    private string _audioClipName = "hero_jump";
    public int _jumpCountLeft { get; private set; }
    private bool CanJump { get; set; }
    public JumpState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
        CanJump = true;
    }
    public override void Enter()
    {
        base.Enter();
        ReduceJumpCountNum();
        _core.Get<RgMoveComponent>().SetYVelocity(_data.jumpVelocity);
        _fsm.GetState<InAirState>().SetJumpingUpTrue();
        SetAbilityDown();
        _ower.InputHandle.SetJumpInput();
        _ower.CreateJumpEffect();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public void SetCanJump(bool value) => CanJump = value;
    public void ResetJumpCountNum() => _jumpCountLeft = _data.maxJumpNumber;
    public void ReduceJumpCountNum()
    {
        _jumpCountLeft--;
        if (_jumpCountLeft < 0)
        {
            _jumpCountLeft = 0;
            Debug.LogError($"没跳跃次数还能跳跃！");
        }
    }
    public bool CheckIfCanJump() => _jumpCountLeft > 0&&CanJump;
    public override E_CharacterState TargetState() => E_CharacterState.INAIR;
}
