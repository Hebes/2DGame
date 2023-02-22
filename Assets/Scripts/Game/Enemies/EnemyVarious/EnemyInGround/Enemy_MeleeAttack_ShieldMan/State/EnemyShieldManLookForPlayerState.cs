using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManLookForPlayerState : EnemyBaseLookForPlayerState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    public EnemyShieldManLookForPlayerState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isInMaxArgo, E_CharacterState.DETECTED);
        AddTargetState(() => _isLookForOver, E_CharacterState.MOVE);
    }
}
