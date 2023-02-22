using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowChargeState : EnemyBaseChargeState<Enemy_MeleeAttack_Crow, EnemyCrowData>
{
    private string _audioClipName = "enemy_crow_charge";
    public EnemyCrowChargeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Crow ower, EnemyCrowData data) : base(fsm, animBoolName, ower, data)
    {
        //todo 敌人逻辑修改
        AddTargetState(()=>_isWallFront||!_isLedgeVertical||_isChargeOver,E_CharacterState.LOOKFOR);
        AddTargetState(() => _isInMeleeAttack, E_CharacterState.MELEEATTACK);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
