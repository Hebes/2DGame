using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAirAttackState : AbilityBaseState
{
    protected int _xInput;
    protected int _yInput;
    protected float _curMovementVelocity;
    private string _audioClipName = "sword_";
    private int _curCombatIndex;
    public MeleeAirAttackState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _curCombatIndex = Combat.OpenAttack(E_Attack.MELEE_AIR, SetAbilityDown);
        _curMovementVelocity = Combat.GetCurCombatVelocity();
        Move.SetYVelocity(_curMovementVelocity);
        _ower.InputHandle.SetAttackInput();
        PlayAttackAudio(_curCombatIndex);
    }
    private void PlayAttackAudio(int index)
    {
        switch (index)
        {
            case 1:
            case 2:
            case 3:
                AudioMgr.Instance.PlayOnce(_audioClipName + (index - 1));
                break;
        }
    }
    public override void Exit()
    {
        base.Exit();
        Move.SetXVelocity(0);
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_CANFLIP);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _xInput = _ower.InputHandle.XInput;
        _yInput = _ower.InputHandle.YInput;
        Move.CheckCanFlip(_xInput);
    }
    public override E_CharacterState TargetState() => E_CharacterState.INAIR;

}
