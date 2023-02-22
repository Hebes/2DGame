using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurIdleState : BossBaseIdleState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    public BossMinotaurIdleState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>_isIdleOver&&PlayerTf!=null,E_CharacterState.DETECTED);
    }
}
