using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressDeadState : EnemyBaseDeadState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    private string _audiioClipName = "enemy_huntress_dead";
    public EnemyHuntressDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audiioClipName);
    }
}
