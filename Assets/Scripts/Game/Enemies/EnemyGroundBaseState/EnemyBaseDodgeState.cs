using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseDodgeState<T, X> : EnemyAbilityState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    public bool _isFlip { get; protected set; }
    private float _lastDodgeTime;
    public EnemyBaseDodgeState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        CheckIfCanFlip();
        SetDodgeAction();
        SetAbilityDown();
        _lastDodgeTime = Time.time;
    }
    protected virtual void SetDodgeAction()
        => Move.SetVelocityInDir(_data.dodgeStrength, _data.dodgeDir, false);
    public override void Exit()
    {
        base.Exit();
        _isFlip = false;
    }
    protected void CheckIfCanFlip()
    {
        if (_isFlip)
            Move.Flip();
    }
    public void SetFlip(bool value) => _isFlip = value;
    public bool CheckCanDodge()
        => Time.time >= _lastDodgeTime + _data.dodgeCoolDownTime;
}
