using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAttack_Mushroom : EnemyBase<EnemyMushroomData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.MOVE,new EnemyMushroomMoveState(Fsm,Consts.CHARACTER_ANM_MOVE,this,_data));
        Fsm.AddState(E_CharacterState.IDLE,new EnemyMushroomIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new EnemyMushroomHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemyMushroomDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.INAIR,new EnemyMushroomInAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.EnterFirstState();
    }
}
