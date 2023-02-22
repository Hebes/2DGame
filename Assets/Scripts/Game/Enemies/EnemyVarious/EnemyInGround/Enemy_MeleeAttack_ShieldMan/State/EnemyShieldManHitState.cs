using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShieldManHitState : EnemyBaseHitState<Enemy_MeleeAttack_ShieldMan, EnemyShieldManData>
{
    protected bool _isCanEnterShieldState;
    public EnemyShieldManHitState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_ShieldMan ower, EnemyShieldManData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => AnimFinish && !_isGround, E_CharacterState.INAIR);
        AddTargetState(() => AnimFinish && _isCanEnterShieldState && fsm.GetState<EnemyShieldManShieldState>().CheckCanShield(), E_CharacterState.SHIELD);
        AddTargetState(() => AnimFinish, E_CharacterState.MELEEATTACK);
    }
    public override void Enter()
    {
        base.Enter();
        _isCanEnterShieldState = Random.Range(0,100)<_data.hitToShieldStateProperty;
    }
}
