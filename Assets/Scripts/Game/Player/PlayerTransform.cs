using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform :NormalSingleTon<PlayerTransform>
{
    private Transform _playerTf;
    public Transform GetPlayerTf
    {
        get
        {
            if (_playerTf==null)
            {
                var playerGo = GameObject.FindGameObjectWithTag(Tags.Player);
                if (playerGo != null)
                    _playerTf = playerGo.transform;
            }
            return _playerTf;
        }
    } 
}
