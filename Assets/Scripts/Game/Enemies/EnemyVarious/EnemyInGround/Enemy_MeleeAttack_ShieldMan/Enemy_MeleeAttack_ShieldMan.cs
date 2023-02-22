/****************************************************
    文件：Enemy_MeleeAttack_ShieldMan.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/16 13:57:28
	功能：近战攻击敌人_盾男
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_MeleeAttack_ShieldMan : EnemyBase<EnemyShieldManData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.MOVE,new EnemyShieldManMoveState(Fsm,Consts.CHARACTER_ANM_MOVE,this,_data));
        Fsm.AddState(E_CharacterState.IDLE,new EnemyShieldManIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.DETECTED,new EnemyShieldManDetectedState(Fsm,Consts.CHARACTER_ANM_DETECTED,this,_data));
        Fsm.AddState(E_CharacterState.INAIR,new EnemyShieldManInAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.CHARGE,new EnemyShieldManChargeState(Fsm,Consts.CHARACTER_ANM_CHARGE,this,_data));
        Fsm.AddState(E_CharacterState.HIT,new EnemyShieldManHitState(Fsm,Consts.CHARACTER_ANM_HIT,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemyShieldManDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACK,new EnemyShieldManMeleeAttackState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.MELEEAIRATTACK,new EnemyShieldManMeleeAirAttackState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.LOOKFOR,new EnemyShieldManLookForPlayerState(Fsm,Consts.CHARACTER_ANM_LOOKFOR,this,_data));
        Fsm.AddState(E_CharacterState.DASH,new EnemyShieldManDashState(Fsm,Consts.CHARACTER_ANM_DASH,this,_data));
        Fsm.AddState(E_CharacterState.SHIELD,new EnemyShieldManShieldState(Fsm,Consts.CHARACTER_ANM_SHIELD,this,_data));
        Fsm.AddState(E_CharacterState.LAND,new EnemyShieldManLandState(Fsm,Consts.CHARACTER_ANM_LAND,this,_data));
        Fsm.AddState(E_CharacterState.DODGE,new EnemyShieldManDodgeState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.EnterFirstState();
    }
}