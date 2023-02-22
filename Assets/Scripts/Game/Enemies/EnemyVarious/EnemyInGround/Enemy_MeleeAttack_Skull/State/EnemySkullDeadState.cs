using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullDeadState : EnemyBaseDeadState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    private string _audioClipName = "enemy_skull_dead";
    public EnemySkullDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void DeadAction()
    {
        base.DeadAction();
        CreateDeadSprite();
    }
}
