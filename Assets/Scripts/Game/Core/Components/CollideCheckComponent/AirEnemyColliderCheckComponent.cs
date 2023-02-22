using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class AirEnemyColliderCheckComponent : ComponentBase
{
    [TabGroup("各个检查的设置"),LabelWidth(150),SerializeField]
    private Transform _checkPlayerPos;
    [TabGroup("各个检查的设置"), LabelWidth(150), SerializeField]
    private Transform _checkGroundPos;



    [TabGroup("各个检测范围"), LabelWidth(150), SerializeField]
    private float _groundCheckDis;
    [TabGroup("各个检测范围"), LabelWidth(150), SerializeField]
    private float _minArgoCheckDis;
    [TabGroup("各个检测范围"), LabelWidth(150), SerializeField]
    private float _maxArgoCheckDis;
    [TabGroup("各个检测范围"), LabelWidth(150), SerializeField]
    private float _meleeAttackDis;
    [TabGroup("各个检测范围"), LabelWidth(150), SerializeField]
    private float _rangeAttackDis;



    [TabGroup("相应层级设置"), LabelWidth(150), SerializeField]
    private LayerMask _groundLayer;
    [TabGroup("相应层级设置"), LabelWidth(150), SerializeField]
    private LayerMask _playerLayer;
    [TabGroup("相应层级设置"), LabelWidth(150), SerializeField]
    private LayerMask _bulletLayer;



    public virtual bool Ground
        => Physics2D.OverlapCircle(_checkGroundPos.position, _groundCheckDis, _groundLayer);
    public virtual bool PlayerInMinArgo
        => Physics2D.OverlapCircle(_checkPlayerPos.position, _minArgoCheckDis, _playerLayer);
    public virtual bool PlayerInMaxArgo
        => Physics2D.OverlapCircle(_checkPlayerPos.position, _maxArgoCheckDis, _playerLayer);
    public virtual bool PlayerInMeleeAttackDis
        => Physics2D.OverlapCircle(_checkPlayerPos.position, _meleeAttackDis, _playerLayer);
    public virtual bool PlayerInRangeAttackDis
        => Physics2D.OverlapCircle(_checkPlayerPos.position, _rangeAttackDis, _playerLayer);
    public virtual bool BulletInMeleeAttackDis
        => Physics2D.OverlapCircle(_checkPlayerPos.position, _meleeAttackDis*3, _bulletLayer);
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_checkGroundPos != null)
        {
            Gizmos.DrawWireSphere(_checkGroundPos.transform.position,_groundCheckDis);
        }
        if (_checkPlayerPos != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_checkPlayerPos.transform.position, _minArgoCheckDis);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_checkPlayerPos.transform.position, _maxArgoCheckDis);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_checkPlayerPos.transform.position, _meleeAttackDis);
            Gizmos.DrawWireSphere(_checkPlayerPos.transform.position, _rangeAttackDis);
        }
    }
#endif
}
