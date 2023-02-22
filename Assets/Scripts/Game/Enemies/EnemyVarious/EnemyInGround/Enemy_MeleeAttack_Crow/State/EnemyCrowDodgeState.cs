using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowDodgeState : EnemyBaseDodgeState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    private string _audioClipName = "enemy_crow_jump";
    public EnemyCrowDodgeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isAbilityDown,E_CharacterState.INAIR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
