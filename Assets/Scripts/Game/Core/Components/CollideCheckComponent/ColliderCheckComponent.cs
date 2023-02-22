using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ColliderCheckComponent : ComponentBase
{
    [TabGroup("各个检查点的配置"), LabelWidth(120), SerializeField]
    private Transform _groundCheckTf;
    [TabGroup("各个检查点的配置"), LabelWidth(120), SerializeField]
    public Transform _wallFrontCheckTf;
    [TabGroup("各个检查点的配置"), LabelWidth(120), SerializeField]
    private Transform _wallBackCheckTf;
    [TabGroup("各个检查点的配置"), LabelWidth(160), SerializeField]
    public Transform _ledgeHorizontalCheckTf;
    [TabGroup("各个检查点的配置"), LabelWidth(160), SerializeField]
    private Transform _ledgeVerticalCheckTf;
    [TabGroup("各个检查点的配置"), LabelWidth(120), SerializeField]
    private Transform _ceilingCheckTf;



    [TabGroup("各个检测范围"), LabelWidth(200)]
    public float _groundAndCelingCheckRadius = 0.3f;
    [TabGroup("各个检测范围"), LabelWidth(200)]
    public float _wallAndLedgeCheckDis = 0.5f;



    [TabGroup("相应层级设置"), LabelWidth(120)]
    public LayerMask _groundLayer;
    public virtual bool Ground => Physics2D.OverlapCircle(_groundCheckTf.position, _groundAndCelingCheckRadius, _groundLayer);
    public virtual bool Ceiling => Physics2D.OverlapCircle(_ceilingCheckTf.position, _groundAndCelingCheckRadius, _groundLayer);
    public virtual bool WallFront => Physics2D.Raycast(_wallFrontCheckTf.position, _owerTf.right, _wallAndLedgeCheckDis, _groundLayer);
    public virtual bool WallBack => Physics2D.Raycast(_wallBackCheckTf.position, -_owerTf.right, _wallAndLedgeCheckDis, _groundLayer);
    public virtual bool LedgeHorizontal => Physics2D.Raycast(_ledgeHorizontalCheckTf.position, _owerTf.right, _wallAndLedgeCheckDis, _groundLayer);
    public virtual bool LedgeVerticalFront => Physics2D.Raycast(_ledgeVerticalCheckTf.position, -_owerTf.up, _wallAndLedgeCheckDis, _groundLayer);
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_groundCheckTf != null)
            Gizmos.DrawWireSphere(_groundCheckTf.position, _groundAndCelingCheckRadius);
        if (_ceilingCheckTf != null)
            Gizmos.DrawWireSphere(_ceilingCheckTf.position, _groundAndCelingCheckRadius);
        Gizmos.color = Color.white;
        if (_owerTf == null)
            return;
        if (_wallFrontCheckTf != null)
            Gizmos.DrawLine(_wallFrontCheckTf.position, _wallFrontCheckTf.position + (Vector3)_owerTf?.right * _wallAndLedgeCheckDis);
        if (_wallBackCheckTf != null)
            Gizmos.DrawLine(_wallBackCheckTf.position, _wallBackCheckTf.position - (Vector3)_owerTf?.right * _wallAndLedgeCheckDis);
        if (_ledgeHorizontalCheckTf != null)
            Gizmos.DrawLine(_ledgeHorizontalCheckTf.position, _ledgeHorizontalCheckTf.position + (Vector3)_owerTf?.right * _wallAndLedgeCheckDis);
        if (_ledgeVerticalCheckTf != null)
            Gizmos.DrawLine(_ledgeVerticalCheckTf.position, _ledgeVerticalCheckTf.position - (Vector3)_owerTf?.up * _wallAndLedgeCheckDis);
    }
#endif
}
