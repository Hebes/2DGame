using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyShieldManDeadState : EnemyBaseDeadState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    private string _audioClipName = "enemy_shieldMan_dead";
    public EnemyShieldManDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
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
