using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullDodgeState : EnemyBaseDodgeState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    private string _audioClipName = "enemy_skull_jump";
    public EnemySkullDodgeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isAbilityDown,E_CharacterState.INAIR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
