using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomDeadState : EnemyBaseDeadState<Enemy_MeleeAttack_Mushroom, EnemyMushroomData>
{
    private string _auidoClioName = "enemy_mushroom_dead";
    public EnemyMushroomDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Mushroom ower, EnemyMushroomData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
    protected override void DeadAction()
    {
        base.DeadAction();
        CreateDeadSprite();
    }
}
