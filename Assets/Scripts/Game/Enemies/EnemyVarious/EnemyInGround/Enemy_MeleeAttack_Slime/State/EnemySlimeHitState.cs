using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeHitState : EnemyBaseHitState<Enemy_MeleeAttack_Slime, EnemySlimeData>
{
    private string _audioClipName = "enemy_slime_split";
    public EnemySlimeHitState(FiniteStateMachine fsm, string animBoolName, Enemy_MeleeAttack_Slime ower, EnemySlimeData data) : base(fsm, animBoolName, ower, data)
    {
        AddTargetState(()=>AnimFinish,E_CharacterState.BACK);
    }
    public override void Enter()
    {
        base.Enter();
        CopySelf();
    }
    //复制自身
    private void CopySelf()
    {
        if (Random.Range(0, 100) < _data.slimeCopyProbability)
        {
            var slimeGo = GameObject.Instantiate(_data.slimePrefab);
            slimeGo.transform.position = _ower.transform.position;
            slimeGo.transform.position += (Vector3)_data.slimeCreateOffset*Move.FacingDir;
            slimeGo.transform.rotation = _ower.transform.rotation;
            AudioMgr.Instance.PlayOnce(_audioClipName);
        }
    }
}
