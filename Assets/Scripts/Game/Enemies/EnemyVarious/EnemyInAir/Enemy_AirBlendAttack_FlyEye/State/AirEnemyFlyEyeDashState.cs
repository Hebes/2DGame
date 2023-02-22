using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeDashState : AirEnemyBaseDashState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    private string _auidoClioName = "enemy_flyeye_dash";
    public AirEnemyFlyEyeDashState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isPlayerInMeleeAttack,E_CharacterState.MELEEATTACK);
        AddTargetState(() => !_isDash, E_CharacterState.BACK);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
}
