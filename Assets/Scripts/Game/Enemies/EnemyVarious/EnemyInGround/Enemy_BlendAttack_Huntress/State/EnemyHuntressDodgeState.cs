using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHuntressDodgeState : EnemyBaseDodgeState<Enemy_BlendAttack_Huntress, EnemyHuntressData>
{
    private string _audiioClipName = "enemy_huntress_jump";
    protected Vector2 _curDir;
    public EnemyHuntressDodgeState(FiniteStateMachine fsm, string animBoolName, Enemy_BlendAttack_Huntress ower, EnemyHuntressData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(() => _isAbilityDown, E_CharacterState.INAIR);
    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audiioClipName);
    }
    protected override void SetDodgeAction()
    {
        if (PlayerTf == null)
            return;
            _curDir.x = PlayerTf.position.x > _ower.transform.position.x ? 1 : -1;
        _curDir.y = 2;
        Move.LookAtPlayer();
        Move.SetVelocity(_data.dodgeStrength, _curDir);
    }
}
