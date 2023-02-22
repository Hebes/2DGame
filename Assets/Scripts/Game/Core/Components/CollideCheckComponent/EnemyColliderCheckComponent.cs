using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class EnemyColliderCheckComponent : ColliderCheckComponent
{
    [TabGroup("相应层级设置"), LabelWidth(120),SerializeField]
    private LayerMask _whatIsBullet;
    [TabGroup("相应层级设置"), LabelWidth(120), SerializeField]
    private LayerMask _whatIsPlayer;



    [TabGroup("各个检查点的配置"), LabelWidth(120),SerializeField]
    private Transform _playerCheckPos;//检测主角的位置
    [TabGroup("各个检查点的配置"), LabelWidth(200), SerializeField]
    private Transform _ledgeVerticalBackPos;
    [TabGroup("各个检查点的配置"), LabelWidth(200), SerializeField]
    private Transform _rangeLedgeVerticalFront;
    [TabGroup("各个检查点的配置"), LabelWidth(200), SerializeField]
    private Transform _rangeLedgeVerticalBack;
    [TabGroup("各个检查点的配置"), LabelWidth(200), SerializeField]
    private Transform _playerInAirCheckPos;//检测主角是否在空中的检测点





    [TabGroup("各个检测范围"), LabelWidth(200),SerializeField]
    private float _meleeAttackDis;//在近战攻击范围内
    [TabGroup("各个检测范围"), LabelWidth(200), SerializeField]
    private float _rangeAttackDis;//在远程攻击范围内
    [TabGroup("各个检测范围"), LabelWidth(200), SerializeField]
    private float _minArgoDis;//进入作战状态的最小距离
    [TabGroup("各个检测范围"), LabelWidth(200), SerializeField]
    private float _maxArgoDis;//可以观察到主角的最大距离
    [TabGroup("各个检测范围"), LabelWidth(200), SerializeField]
    private float _boxWidth;//盒状检测的宽度
    [TabGroup("各个检测范围"), LabelWidth(200), SerializeField]



    private float _checkPlayerInBackDis;
    #region 在空中的敌人检测相关
    [TabGroup("空中各个检测范围"), LabelWidth(200), SerializeField]
    private float _airMinArgoRadius;//在空中检测主角的最小警惕半径
    [TabGroup("空中各个检测范围"), LabelWidth(200), SerializeField]
    private float _airMaxArgoRadius;//在空中检测主角的最大半径
    #endregion
    public bool PlayerInMeleeAttackDis => Physics2D.OverlapBox(_playerCheckPos.position + _owerTf.right * _meleeAttackDis / 2, new Vector2(_meleeAttackDis, _boxWidth), 0, _whatIsPlayer);
    public bool PlayerInRangeAttackDis => Physics2D.OverlapBox(_playerCheckPos.position + _owerTf.right * _rangeAttackDis / 2, new Vector2(_rangeAttackDis, _boxWidth), 0, _whatIsPlayer);
    public bool PlayerInMinArgo => Physics2D.OverlapBox(_playerCheckPos.position + _owerTf.right * _minArgoDis / 2, new Vector2(_minArgoDis, _boxWidth), 0, _whatIsPlayer);
    public bool PlayerInMaxArgo => Physics2D.OverlapBox(_playerCheckPos.position + _owerTf.right * _maxArgoDis / 2, new Vector2(_maxArgoDis, _boxWidth), 0, _whatIsPlayer);
    public bool PlayerInBack => Physics2D.OverlapBox(_playerCheckPos.position - _owerTf.right * _checkPlayerInBackDis / 2, new Vector2(_checkPlayerInBackDis, _boxWidth), 0, _whatIsPlayer);
    //检测主角是否在空中
    public bool PlayerInAir => Physics2D.OverlapBox(_playerInAirCheckPos.position + _owerTf.right * _minArgoDis / 2, new Vector2(_minArgoDis, _boxWidth), 0, _whatIsPlayer);
    public bool LedgeVerticalBack => Physics2D.Raycast(_ledgeVerticalBackPos.position, -_owerTf.up, _wallAndLedgeCheckDis, _groundLayer);
    public bool RangeLedgeVerticalFront => Physics2D.Raycast(_rangeLedgeVerticalFront.position, -_owerTf.up, _wallAndLedgeCheckDis, _groundLayer);
    public bool RangeLedgeVerticalBack => Physics2D.Raycast(_rangeLedgeVerticalBack.position, -_owerTf.up, _wallAndLedgeCheckDis, _groundLayer);
    //检车是否有子弹射击过来 todo需要完善
    public bool BulletInMeleeAttackDis => Physics2D.OverlapBox(_playerCheckPos.position + _owerTf.right * _meleeAttackDis * 3, new Vector2(_meleeAttackDis * 3f, _boxWidth), 0, _whatIsBullet);
    #region 在空中的敌人检测相关


    #endregion
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (_owerTf == null) return;
        Gizmos.DrawLine(_rangeLedgeVerticalFront.position, _rangeLedgeVerticalFront.position - (_owerTf.up * _wallAndLedgeCheckDis));
        Gizmos.DrawLine(_rangeLedgeVerticalBack.position, _rangeLedgeVerticalBack.position - (_owerTf.up * _wallAndLedgeCheckDis));
        Gizmos.DrawLine(_ledgeVerticalBackPos.position, _ledgeVerticalBackPos.position - (_owerTf.up * _wallAndLedgeCheckDis));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_playerCheckPos.position + _owerTf.right * _minArgoDis / 2, new Vector2(_minArgoDis, _boxWidth));
        Gizmos.color = Color.blue;
        if (_playerInAirCheckPos != null)
            Gizmos.DrawWireCube(_playerInAirCheckPos.position + _owerTf.right * _minArgoDis / 2, new Vector2(_minArgoDis, _boxWidth));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_playerCheckPos.position + _owerTf.right * _maxArgoDis / 2, new Vector2(_maxArgoDis, _boxWidth));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_playerCheckPos.position + _owerTf.right * _meleeAttackDis / 2, new Vector2(_meleeAttackDis, _boxWidth));
        Gizmos.DrawWireCube(_playerCheckPos.position + _owerTf.right * _rangeAttackDis / 2, new Vector2(_rangeAttackDis, _boxWidth));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_playerCheckPos.position - _owerTf.right * _checkPlayerInBackDis / 2, new Vector2(_checkPlayerInBackDis, _boxWidth));
        Gizmos.color = Color.white;
    }
#endif
}
