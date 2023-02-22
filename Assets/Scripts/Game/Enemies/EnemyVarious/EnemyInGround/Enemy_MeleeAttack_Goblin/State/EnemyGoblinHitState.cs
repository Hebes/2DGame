using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGoblinHitState : EnemyBaseHitState<Enemy_MeleeAttack_Goblin, EnemyGoblineData>
{
    private string _audioClipScaredName = "enemy_goblin_scared";
    private string _audioClipJumpName = "enemy_goblin_jump";
    public EnemyGoblinHitState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Goblin ower, EnemyGoblineData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.DODGE);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipScaredName);
    }
    public override void Exit()
    {
        base.Exit();
        AudioMgr.Instance.PlayOnce(_audioClipJumpName);
    }
}
