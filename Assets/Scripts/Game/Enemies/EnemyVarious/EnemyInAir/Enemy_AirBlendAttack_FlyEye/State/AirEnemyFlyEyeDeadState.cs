using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyFlyEyeDeadState : AirEnemyBaseDeadState<Enemy_AirBlendAttack_FlyEye, AirEnemyFlyEyeData>
{
    private string _auidoClioName = "enemy_flyeye_dead";
    public AirEnemyFlyEyeDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_AirBlendAttack_FlyEye ower, AirEnemyFlyEyeData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
}
