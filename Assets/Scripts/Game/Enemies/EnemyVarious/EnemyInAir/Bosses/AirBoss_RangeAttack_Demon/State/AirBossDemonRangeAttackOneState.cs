using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBossDemonRangeAttackOneState : AirBossBaseRangeAttackState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private string _audioClipName = "enemy_demon_rangeAttack1";
    public AirBossDemonRangeAttackOneState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isAbilityDown,E_CharacterState.DISAPPEAR,
            ()=>fsm.GetState<AirBossDemonDisappearState>().SetIsisImmediateaToAppearState(true));
        SetRangeAttackCombatIndex(_data.rangeAttackOneCombatIndex);
    }
    public override void Enter()
    {
        base.Enter();
        NavFindingComponent.SetStopDis(_data.rangeAttackOneStopDis);
        NavFindingComponent.SetCurVelocity(_data.rangeAttackOneVelocity);
        if (Move.PlayerTf != null)
            NavFindingComponent.SetDestination(Move.PlayerTf.position);
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.LookAtPlayer();
    }
    public override void Exit()
    {
        base.Exit();
        NavFindingComponent.StopFindPath();
    }
    //该远程攻击 不发射子弹
    protected override void SpawnRangeAttack()
    {


    }
}
