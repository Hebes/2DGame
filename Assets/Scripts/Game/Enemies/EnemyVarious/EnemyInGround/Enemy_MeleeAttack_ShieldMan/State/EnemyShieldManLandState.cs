using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManLandState : EnemyBaseLandState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    public EnemyShieldManLandState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => AnimFinish, E_CharacterState.DETECTED);
    }
}