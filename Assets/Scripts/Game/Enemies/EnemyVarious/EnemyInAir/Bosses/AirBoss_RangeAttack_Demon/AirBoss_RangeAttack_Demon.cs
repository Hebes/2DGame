/****************************************************
    文件：AirBoss_RangeAttack_Demon.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/22 15:50:55
	功能：空中BossDemon
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AirBoss_RangeAttack_Demon : BossBase<AirBossDemonData>
{
    protected override void InitState()
    {
        Fsm.AddState(E_CharacterState.APPEAR,new AirBossDemonAppearState(Fsm,Consts.CHARACTER_ANM_APPEAR,this,_data));
        Fsm.AddState(E_CharacterState.DETECTED,new AirBossDemonDetectedState(Fsm,Consts.CHARACTER_ANM_DETECTED,this,_data));
        Fsm.AddState(E_CharacterState.CHARGE,new AirBossDemonChargeState(Fsm,Consts.CHARACTER_ANM_CHARGE,this,_data));
        Fsm.AddState(E_CharacterState.IDLE, new AirBossDemonIdleState(Fsm,Consts.CHARACTER_ANM_IDLE,this,_data));
        Fsm.AddState(E_CharacterState.DEAD, new AirBossDemonDeadState(Fsm,Consts.CHARACTER_ANM_DEAD,this,_data));
        Fsm.AddState(E_CharacterState.RANGEATTACK, new AirBossDemonRangeAttackOneState(Fsm,Consts.CHARACTER_ANIM_RANGEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.RANGEATTACKTWO, new AirBossDemonRangeAttackTwoState(Fsm,Consts.CHARACTER_ANIM_RANGEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.RANGEATTACKTHREE, new AirBossDemonRangeAttackThreeState(Fsm,Consts.CHARACTER_ANIM_RANGEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.RANGEATTACKFOUR, new AirBossDemonRangeAttackFourState(Fsm,Consts.CHARACTER_ANIM_RANGEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.DISAPPEAR, new AirBossDemonDisappearState(Fsm,Consts.CHARACTER_ANM_DISAPPEAR,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACK, new AirBossDemonMeleeAttackOneState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.MELEEATTACKTWO, new AirBossDemonMeleeAttackTwoState(Fsm,Consts.CHARACTER_ANM_MELEEATTACK,this,_data));
        Fsm.AddState(E_CharacterState.RIGIDITY, new AirBossDemenRigidityState(Fsm,Consts.CHARACTER_ANM_RIGIDITY,this,_data));
        Fsm.EnterFirstState();
    }
    protected override void InitComponent()
    {
        base.InitComponent();
        //设置为无敌状态
        Core.Get<EnemyBehaviorComponent>().SetDomineering(true);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //Debug.Log(Fsm.CurStateName);
    }
}