using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWarriorRigidityState : EnemyBaseRigidityState<Enemy_MeleeAttack_Warrior, EnemyWarriorData>
{
    private bool _grounded;
    private string _audioClipName = "enemy_warrior_rigidity";
    public EnemyWarriorRigidityState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Warrior ower, EnemyWarriorData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isRigidityOver && !_grounded, E_CharacterState.INAIR);
        AddTargetState(() => _isRigidityOver, E_CharacterState.BACK);
    }
    public override void Check()
    {
        base.Check();
        _grounded = ColliderCheck.Ground;
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    public override void Exit()
    {
        base.Exit();
        Behavior.SetDomineering(true);
    }
}
