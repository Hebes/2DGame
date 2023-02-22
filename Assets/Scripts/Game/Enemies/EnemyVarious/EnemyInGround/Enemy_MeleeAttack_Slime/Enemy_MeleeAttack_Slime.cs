using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeleeAttack_Slime : EnemyBase<EnemySlimeData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.IDLE,new EnemySlimeIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.MOVE,new EnemySlimeMoveState(Fsm,Consts.CHARACTER_ANM_MOVE,this,_data));
        Fsm.AddState(E_CharacterState.BACK,new EnemySlimeBackState(Fsm,Consts.CHARACTER_ANM_BACK,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new EnemySlimeHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemySlimeDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.INAIR,new EnemySlimeInAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.EnterFirstState();
    }
}
