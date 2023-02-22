using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblineDodgeState : EnemyBaseDodgeState<Enemy_MeleeAttack_Goblin, EnemyGoblineData>
{
    private bool _isEnterScaredState;//是否进入受惊状态
    public EnemyGoblineDodgeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Goblin ower, EnemyGoblineData data) : base(fsm, animBoolName, ower, data)
    {
        SetIsEnterAfterInAirScaredState(true);//默认情况下进入受惊状态
        AddTargetState(()=>_isAbilityDown,E_CharacterState.INAIR,()=>fsm.GetState<EnemyGoblinInAirState>().SetIsEnterScaredState(_isEnterScaredState));
    }
    public override void Enter()
    {
        base.Enter();
        Move.LookAtPlayerBetween();
        SetIsEnterAfterInAirScaredState(true);
    }
    public void SetIsEnterAfterInAirScaredState(bool value)
      => _isEnterScaredState = value;
}
