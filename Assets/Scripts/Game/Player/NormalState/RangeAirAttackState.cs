using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAirAttackState : AbilityBaseState
{
    protected int _xInpput;
    private string _audioClipName = "hero_bow";
    public RangeAirAttackState(FiniteStateMachine fsm, string animBoolName, HeroMan ower, HeroManData data) : base(fsm, animBoolName, ower, data)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Combat.OpenAttack(E_Attack.RANGE_AIR, SetAbilityDown);
        Move.SetXVelocity(0);//需要设置x方向上的速度为0
        Move.SetYVelocity(_data.rangeAttackVeloctiy);
    }
    public override void Exit()
    {
        base.Exit();
        SubEventMgr.ExecuteEvent(E_EventName.CHARACTER_CANFLIP);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        _xInpput = _ower.InputHandle.XInput;
    }
    public override E_CharacterState TargetState() => E_CharacterState.INAIR;
    private void SpawnArrow()
    {
        if (!Combat.GetCombatActive())
            return;
        var prefab = Combat.GetRangeAttackPrefab();
        var prefabGo = PoolManager.Instance.GetFromPool(prefab);
        var bulletArrow = prefabGo.GetComponent<BulletHeroArrow>();
        //bool isFlip = false;
        //if (Move.FacingDir == -1)
        //    isFlip = true;
        var pos = _ower.transform.position;
        bulletArrow.Init(_ower.transform.right, Behavior.GetGroup(), Combat.GetHostileGroup(), pos/*, isFlip*/);
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void SetAbilityDown()
    {
        base.SetAbilityDown();
        if (Combat.CheckCanRangeAttack(-_data.oneRangeAttackMagicPowerSpend))
            SpawnArrow();
    }
}
