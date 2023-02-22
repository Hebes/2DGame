using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressRangeAttackState : EnemyBaseRangeAttackState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    private string _audioClipName = "enemy_huntress_rangeAttack";
    public EnemyHuntressRangeAttackState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown && CheckCanEnterRangeAttack(), E_CharacterState.RANGEATTACK);
        AddTargetState(() => _isAbilityDown, E_CharacterState.DETECTED);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName + _combatIndex);
    }
    protected override Vector3 GetSpawnBulletDir()
    {
        if (PlayerTf != null)
            return (PlayerPos - _ower.transform.position).normalized;
        return Vector3.zero;
    }
}
