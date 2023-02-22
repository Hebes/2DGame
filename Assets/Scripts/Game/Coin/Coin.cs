/****************************************************
    文件：Coin.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/25 14:54:35
	功能：游戏金币脚本
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour
{
    private Collider2D _collider;
    private Collider2D _colliderTwo;
    private Rigidbody2D _rb;
    private float _maxXStartSpeed = 5f;
    private float _minXStartSpeed = -5f;
    private float _maxYStartSpeed = 20f;
    private float _minYStartSpeed = 10f;
    private GameObject _trailEffect;//拖尾特效
    private float _moveSpeed = 18;//被主角所吸引后的移动速度
    private Transform _playerTf;

    private bool _isAttracting;
    private bool IsAttracting
    {
        get => _isAttracting;
        set
        {
            _rb.isKinematic = value;
            _isAttracting = value;
            _colliderTwo.enabled = !value;
            SetTrailEffectActive(value);
        }
    }
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _colliderTwo = transform.Find("Collider").GetComponent<Collider2D>();
        _rb =GetComponent<Rigidbody2D>();
        _playerTf = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        _trailEffect = transform.Find("TrailEffect").gameObject;
        EventMgr.Instance.AddEvent(E_EventName.CHARACTER_ATTRACTCOIN, CheckCanAttracked);
    }
    private void Update()
    {
        if (_playerTf == null || !IsAttracting)
            return;
        transform.right = (_playerTf.position - transform.position).normalized;
        transform.Translate(transform.right * _moveSpeed * Time.deltaTime, Space.World);
    }
    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEvent(E_EventName.CHARACTER_ATTRACTCOIN, CheckCanAttracked);
    }
    private void OnEnable()
    {
        IsAttracting = false;
        SetColliderEnable(true);
        AddRandomForce();
    }
    //添加随机力
    private void AddRandomForce()
    {
        var xSpeed = Random.Range(_minXStartSpeed, _maxXStartSpeed);
        var ySpeed = Random.Range(_minYStartSpeed, _maxYStartSpeed);
        _rb.velocity = new Vector2(xSpeed, ySpeed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            CreateEffect();
            GameDataModel.Instance.AddOrReduceCoin(1);
            SetColliderEnable(false);
            IsAttracting = false;
            PoolManager.Instance.PushPool(gameObject);
        }
    }
    private void SetColliderEnable(bool value)
        => _collider.enabled = value;
    //检测是否可以被主角所吸引
    private void CheckCanAttracked(params object[] args)
    {
        if (_playerTf == null || IsAttracting || !gameObject.activeSelf)
            return;
        var maxDis = float.Parse(args[0].ToString());
        var dis = Vector2.Distance(_playerTf.position, transform.position);
        if (dis < maxDis)
        {
            _rb.velocity = Vector2.zero;
            IsAttracting = true;
        }
    }
    private void CreateEffect()
    {
        var effect = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_TOUCHINGCOIN);
        effect.transform.position = transform.position;
        effect.AddOrGet<AutoRecycleComponent>().Init(1f);
    }
    private void SetTrailEffectActive(bool value)
        => _trailEffect.gameObject.SetActive(value);
}