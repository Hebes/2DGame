using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyGoblinInAirState : EnemyBaseInAirState<Enemy_MeleeAttack_Goblin, EnemyGoblineData>
{
    private bool _isEnterScaredState;//是否进入受惊状态
    public EnemyGoblinInAirState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Goblin ower, EnemyGoblineData data) : base(fsm, animBoolName, ower, data)
    {
        SetIsEnterScaredState(true);
        AddTargetState(()=>_isDead,E_CharacterState.DEAD);
        AddTargetState(()=> _isOnGround && Move.GetCurVelocity.y <= 0.01f&&!_isEnterScaredState,E_CharacterState.MOVE);
        AddTargetState(()=>_isOnGround&&Move.GetCurVelocity.y<=0.01f,E_CharacterState.SCARED);
    }
    public override void Enter()
    {
        base.Enter();
        SetIsEnterScaredState(true);
    }
    public void SetIsEnterScaredState(bool value)
        => _isEnterScaredState = value;
    public override void ActionUpdate()
    {
        base.ActionUpdate();
        Move.LookAtPlayerBetween();
    }
}
