using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyWarriorDeadState : EnemyBaseDeadState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    private string _audioClipName = "enemy_warrior_dead";
    public EnemyWarriorDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
