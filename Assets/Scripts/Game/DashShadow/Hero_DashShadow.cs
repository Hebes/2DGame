using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_DashShadow : MonoBehaviour
{
    [Header("褪色参数")]
    [SerializeField]
    private float _startAlpha=0.8f;
    [SerializeField]
    private float _fadeSpeed=0.1f;
    private Color _targetColor;


    private float _curAlpha;
    private static Transform _playerTf;
    private static SpriteRenderer _playerSpriteRender;
    private static SpriteRenderer PlayerSpriteRender
    {
        get
        {
            if (_playerSpriteRender == null)
            {
                _playerTf = GameObject.FindGameObjectWithTag(Tags.Player).transform;
                _playerSpriteRender = _playerTf.GetComponent<SpriteRenderer>();
            }
            return _playerSpriteRender;
        }
    }
    private SpriteRenderer _render;
    private void Awake()
    {
        _render = GetComponent<SpriteRenderer>();
        _targetColor = _render.color;
    }
    private void OnEnable()
    {
        if (PlayerSpriteRender == null)
            return;
        _render.sprite = PlayerSpriteRender.sprite;
        _targetColor = new Color(1,1,1,_startAlpha);
        _render.color = _targetColor;
        _curAlpha = _startAlpha;
        transform.position = _playerTf.position;
    }
    private void Update()
    {
        if (_curAlpha>0f&&gameObject.activeSelf)
        {
            _curAlpha -= Time.deltaTime * _fadeSpeed;
            _targetColor = new Color(1, 1, 1, _curAlpha);
            _render.color = _targetColor;
            if (_curAlpha <= 0f)
                PoolManager.Instance.PushPool(gameObject);
        }
    }
}
