using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAirAttack_Bat : EnemyBase<AirEnemyBatData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.IDLE,new AirEnemyBatIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new AirEnemyBatHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new AirEnemyBatDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.EnterFirstState();
    }
}
