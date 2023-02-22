using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkullBackState : EnemyBaseBackState<Enemy_MeleeAttack_Skull, EnemySkullData>
{
    private string _audioClipName = "enemy_skull_dash_back";
    public EnemySkullBackState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Skull ower, EnemySkullData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isInMeleeAttack,E_CharacterState.MELEEATTACK);
        AddTargetState(()=>_isBackOver,E_CharacterState.DETECTED);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
