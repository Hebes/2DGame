using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWormDeadState : EnemyBaseDeadState<Enemy_RangeAttack_FireWorm, EnemyFireWormData>
{
    private string _auidoClioName = "enemy_fireworm_dead";
    public EnemyFireWormDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_RangeAttack_FireWorm ower, EnemyFireWormData data) : base(fsm, animBoolName, ower, data)
    {

    }
    protected override void DeadAction()
    {
        base.DeadAction();
        CreateDeadSprite();
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
}
