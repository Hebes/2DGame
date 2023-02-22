using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullDashState : EnemyBaseDashState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    private string _audioClipName = "enemy_skull_dash_back";
    public EnemySkullDashState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isPlayerInMeleeAttack /*&& fsm.GetState<EnemySkullMeleeAttackState>().CheckCanMeleeAttack()*/, E_CharacterState.MELEEATTACK);
        AddTargetState(()=>_isDashOver||_isWallFront||!_isLedgeVerticalFront,E_CharacterState.DETECTED);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
