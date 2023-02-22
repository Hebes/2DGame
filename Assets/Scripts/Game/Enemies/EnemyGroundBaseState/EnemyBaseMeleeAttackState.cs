using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseMeleeAttackState<T, X> : EnemyAbilityState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected bool _isPlayerInBack;
    protected float _curMovementVelocity;
    protected bool _isSetDodgeDir;//是否已经设置Dodge的方向
    protected bool _isRangeLedgeVerticalFront;
    protected bool _isRangeLedgeVerticalBack;
    protected bool _isWallFront;
    protected bool _isWallBack;
    protected bool _isLedgeVerticalFront;
    protected bool _isLedgeVerticalBack;
    protected bool _isInMeleeAttack;
    protected int _combatIndex;//默认攻击索引为1
    protected float _lastMeleeAttackContinueOverTime = float.NegativeInfinity;//上一次连续攻击进入CD的时间
    protected float _lastMeleeAttackExitTime=float.NegativeInfinity;//上一次攻击结束的时间
    public EnemyBaseMeleeAttackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {
        //默认的第一次近战攻击索引为1
        SetAttackCombatIndex(1);
    }
    public override void Enter()
    {
        base.Enter();
        _isSetDodgeDir = false;
        Combat.OpenAttack(E_Attack.MELEE, SetAbilityDown,_combatIndex);
        _curMovementVelocity = Combat.GetCurCombatVelocity();
        Move.SetXVelocityInFacing(_curMovementVelocity, true);
        PlayCurCombatIndexAudio();
        CheckContinueCoolDown();
    }
    public override void Exit()
    {
        base.Exit();
        _lastMeleeAttackExitTime = Time.time;
    }
    protected virtual void PlayCurCombatIndexAudio()
    {

    }
    //每次进入攻击状态都会计算  检测是否进入冷却时间
    private void CheckContinueCoolDown()
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
    //增加攻击索引
    protected void AddAttackIndex()
        => _combatIndex++;
    public override void Check()
    {
        base.Check();
        _isPlayerInBack = ColliderCheck.PlayerInBack;
        _isRangeLedgeVerticalFront = ColliderCheck.RangeLedgeVerticalBack;
        _isRangeLedgeVerticalBack = ColliderCheck.RangeLedgeVerticalBack;
        _isWallFront = ColliderCheck.WallFront;
        _isWallBack = ColliderCheck.WallBack;
        _isLedgeVerticalBack = ColliderCheck.LedgeVerticalBack;
        _isLedgeVerticalFront = ColliderCheck.LedgeVerticalFront;
        _isInMeleeAttack = ColliderCheck.PlayerInMeleeAttackDis;
    }
    //检测是否可以连续近战攻击
    public  bool CheckCanMeleeAttackContinue()
        => Time.time >= _lastMeleeAttackContinueOverTime + _data.meleeAttackContinueCoolDownTime;
    public bool CheckCanMeleeAttack()
        => Time.time > _lastMeleeAttackExitTime + _data.meleeAttackCoolDownTime;
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
