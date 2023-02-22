using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGoblinDeadState : EnemyBaseDeadState<Enemy_MeleeAttack_Goblin, EnemyGoblineData>
{
    private string _audioClipName = "enemy_goblin_dead";
    public EnemyGoblinDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Goblin ower, EnemyGoblineData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
