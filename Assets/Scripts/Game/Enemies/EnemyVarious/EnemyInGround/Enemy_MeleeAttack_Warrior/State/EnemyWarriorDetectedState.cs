using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorDetectedState : EnemyBaseDetectedState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    protected bool _bulletInMeleeAttackDis;
    public EnemyWarriorDetectedState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isInMeleeAttack 
       && fsm.GetState<EnemyWarriorMeleeAttackState>().CheckCanMeleeAttack(), E_CharacterState.MELEEATTACK);
        AddTargetState(() => _bulletInMeleeAttackDis && fsm.GetState<EnemyWarriorDodgeState>().CheckCanDodge(), E_CharacterState.DODGE);
        AddTargetState(() => _inOverMaxWaitTime && _isInMaxAgro, E_CharacterState.CHARGE);
        AddTargetState(() => _inOverMaxWaitTime, E_CharacterState.LOOKFOR);
    }
    public override void Check()
    {
        base.Check();
        _bulletInMeleeAttackDis = ColliderCheck.BulletInMeleeAttackDis;
    }
}
