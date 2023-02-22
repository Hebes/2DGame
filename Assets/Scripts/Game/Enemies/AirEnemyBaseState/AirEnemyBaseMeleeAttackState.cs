using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseMeleeAttackState<T, X> : AirEnemyAbilityState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected bool _isPlayerInMeleeAttackDis;//主角是否在近战攻击范围内

    protected float _curMovementVelocity;
    protected int _combatIndex;
    protected float _lastMeleeAttackContinueOverTime = float.NegativeInfinity;//上一次连续攻击进入CD的时间
    protected float _lastMeleeAttackExitTime = float.NegativeInfinity;//上一次攻击结束的时间
    public AirEnemyBaseMeleeAttackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {
        SetAttackCombatIndex(1);
    }
    public override void Enter()
    {
        base.Enter();
        Combat.OpenAttack(E_Attack.MELEE, SetAbilityDown);
        _curMovementVelocity = Combat.GetCurCombatVelocity();
        Move.SetXVelocityInFacing(_curMovementVelocity, true);
        CheckContinueCanCoolDown();
    }
    public override void Exit()
    {
        base.Exit();
        _lastMeleeAttackExitTime = Time.time;
    }
    public override void Check()
    {
        base.Check();
        _isPlayerInMeleeAttackDis = ColliderCheck.PlayerInMeleeAttackDis;
    }
    //检测是不是可以近战攻击
    public bool CheckCanMeleeAttack()
        => Time.time >= _lastMeleeAttackExitTime + _data.meleeAttackCoolDownTime;
    public bool CheckCanMeleeAttackContinue()
        => Time.time >= _lastMeleeAttackContinueOverTime + _data.meleeAttackContinueCoolDownTime;
    //检测是否进入冷却时间
    private void CheckContinueCanCoolDown()
    {
        //如果此时是强制触发攻击的 则不算
        if (!CheckCanMeleeAttackContinue())
            return;
        //当到达最大连击次数 才会进入冷却时间
        if (_combatIndex >= _data.maxMeleeAttackNumber)
        {
            _lastMeleeAttackContinueOverTime = Time.time;
            SetAttackCombatIndex(1);
        }
    }
    protected override void SetAbilityDown()
    {
        base.SetAbilityDown();
        AnimFinish = true;
    }
    //设置近战攻击的索引
    protected void SetAttackCombatIndex(int index)
    {
        if (index <= 0)
        {
            Debug.LogError($"最小攻击索引为1");
            return;
        }
        _combatIndex = index;
    }
}
