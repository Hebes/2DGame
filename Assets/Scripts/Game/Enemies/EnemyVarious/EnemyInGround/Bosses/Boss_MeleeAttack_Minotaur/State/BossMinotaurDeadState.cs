using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossMinotaurDeadState : BossBaseDeadState<Boss_MeleeAttack_Minotaur, BossMinotaurData>
{
    private string _audioClipName = "enemy_minotaur_dead";
    public BossMinotaurDeadState(FiniteStateMachine fsm, string animBoolName, Boss_MeleeAttack_Minotaur ower, BossMinotaurData data) : base(fsm, animBoolName, ower, data)
    {

    }
    public override void Enter()
    {
        base.Enter();
        AudioMgr.Instance.PlayOnce(_audioClipName);
    }
    protected override void DeadAction()
    {
        base.DeadAction();
        GameStateModel.Instance.CurGameState = E_GameState.Win;
        EventMgr.Instance.ExecuteEvent(E_EventName.BOSS_DEAD,0);
        CreateDeadSprite();
    }
}
