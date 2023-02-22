using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBossDemonMeleeAttackOneState : AirEnemyBaseMeleeAttackState<AirBoss_RangeAttack_Demon, AirBossDemonData>
{
    private Vector2 _dir;
    private string _audioClipName = "enemy_demon_meleeAttack";
    public AirBossDemonMeleeAttackOneState(FiniteStateMachine fsm, string animBoolName, AirBoss_RangeAttack_Demon ower, AirBossDemonData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown, E_CharacterState.MELEEATTACKTWO);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
        ResetDir();
    }
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.SetVelocity(_data.meleeAttackOneVelocity,_dir);
        Move.LookAtPlayer();
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void ResetDir()
        => _dir = (PlayerPos - _ower.transform.position).normalized;
}
