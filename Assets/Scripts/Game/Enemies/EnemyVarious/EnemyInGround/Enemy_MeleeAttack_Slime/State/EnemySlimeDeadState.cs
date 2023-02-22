using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeDeadState : EnemyBaseDeadState<Enemy_MeleeAttack_Slime, EnemySlimeData>
{
    private string _audioClipName = "enemy_slime_dead";
    public EnemySlimeDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Slime ower, EnemySlimeData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
