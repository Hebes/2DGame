using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBatDeadState : AirEnemyBaseDeadState<Enemy_MeleeAirAttack_Bat, AirEnemyBatData>
{
    private string _auidoClioName = "enemy_bat_dead";
    public AirEnemyBatDeadState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAirAttack_Bat ower, AirEnemyBatData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_auidoClioName);
    }
}
