
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurMoveState : BossBaseMoveState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    public BossMinotaurMoveState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {

    }
}
