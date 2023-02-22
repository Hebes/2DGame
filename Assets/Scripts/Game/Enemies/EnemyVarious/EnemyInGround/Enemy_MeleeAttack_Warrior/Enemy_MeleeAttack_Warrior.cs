/****************************************************
    文件：Enemy_MeleeAttack_Warrior.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/31 11:42:7
	功能：近战攻击敌人武士
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_MeleeAttack_Warrior : EnemyBase<EnemyWarriorData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.IDLE,new EnemyWarriorIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.DETECTED,new EnemyWarriorDetectedState(Fsm,Consts.CHARACTER_ANM_DETECTED,this,_data));
        Fsm.AddState(E_CharacterState.CHARGE,new EnemyWarriorChargeState(Fsm,Consts.CHARACTER_ANM_CHARGE,this,_data));
        Fsm.AddState(E_CharacterState.BACK,new EnemyWarriorBackState(Fsm,Consts.CHARACTER_ANM_BACK,this,_data));
        Fsm.AddState(E_CharacterState.DODGE,new EnemyWarriorDodgeState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.LOOKFOR,new EnemyWarriorLookForPlayerState(Fsm,Consts.CHARACTER_ANM_LOOKFOR,this,_data));
        Fsm.AddState(E_CharacterState.INAIR,new EnemyWarriorInAirState(Fsm,Consts.CHARACTER_ANM_INAIR,this,_data));
        Fsm.AddState(E_CharacterState.RIGIDITY,new EnemyWarriorRigidityState(Fsm,Consts.CHARACTER_ANM_RIGIDITY,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACK,new EnemyWarriorMeleeAttackState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.DEAD,new EnemyWarriorDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.EnterFirstState();
    }
    protected override void Awake()
    {
        base.Awake();
        SetDomineering();
    }
}