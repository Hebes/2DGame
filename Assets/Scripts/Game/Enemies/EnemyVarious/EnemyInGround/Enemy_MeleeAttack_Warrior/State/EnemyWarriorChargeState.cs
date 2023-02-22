using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyWarriorChargeState : EnemyBaseChargeState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    protected bool _playerInAir;
    protected bool _bulletInMeleeAttackDis;
    private string _audioClipName = "enemy_warrior_charge_back";
    public EnemyWarriorChargeState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isInMeleeAttack, E_CharacterState.MELEEATTACK);
        AddTargetState(() => _bulletInMeleeAttackDis&&fsm.GetState<EnemyWarriorDodgeState>().CheckCanDodge(), E_CharacterState.DODGE);
        AddTargetState(() => _isChargeOver, E_CharacterState.DETECTED);
    }
    public override void Check()
    {
        base.Check();
        _playerInAir = ColliderCheck.PlayerInAir;
        _bulletInMeleeAttackDis = ColliderCheck.BulletInMeleeAttackDis;
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
}
