using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowDeadState : EnemyBaseDeadState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    private string _audioClipName = "enemy_crow_dead";
    public EnemyCrowDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
