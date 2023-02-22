using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRgMoveComponent : RgMoveComponent
{
    private Transform _playerTf;
    public Transform PlayerTf
    {
        get
        {
            if (_playerTf == null)
                _playerTf = PlayerTransform.Instance.GetPlayerTf;
            return _playerTf;
        }
    }
    protected Transform _target;
    public void SetLookTarget(Transform target) => _target = target;
    public void ClearLookTarget() => _target = null;
    //看向目标
    public void LookAtTarget()
    {
        if (_target == null)
            return;
        if (FacingDir == 1 && _target.position.x < _owerTf.position.x)
            Flip();
        else if (FacingDir == -1 && _target.position.x > _owerTf.position.x)
            Flip();
    }
    //看向主角
    public void LookAtPlayer()
    {
        if(PlayerTf==null)
            return;
        if (FacingDir == 1 && PlayerTf.position.x < _owerTf.position.x)
            Flip();
        else if (FacingDir == -1 && PlayerTf.position.x > _owerTf.position.x)
            Flip();
    }
    public void LookAtPlayerBetween()
    {
        if (PlayerTf == null)
            return;
        if (FacingDir == 1 && PlayerTf.position.x > _owerTf.position.x)
            Flip();
        else if (FacingDir == -1 && PlayerTf.position.x < _owerTf.position.x)
            Flip();
    }
}
