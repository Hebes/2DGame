using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCombatComponent : ComponentBase, ICombat
{
    protected float _damageValue;
    protected Collider2D _collider;
    public override void Init()
    {
        base.Init();
        _collider = GetComponent<Collider2D>();
    }
    public void SetDamage(float damageValue)
        => _damageValue = damageValue;
    protected HashSet<E_Group> _hostileGroupHash;
    public HashSet<E_Group> GetHostileGroup()
        => _hostileGroupHash;
    public void SetHostileGroupHash(HashSet<E_Group> hostileGroup)
        => _hostileGroupHash = hostileGroup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_damageValue == 0)
        {
            Debug.LogError($"子弹战斗组件未初始化,其所有者名称为:{_owerTf.name}");
            return;
        }
        var behavior = collision.GetComponent<IBehavior>();
        if (behavior != null && GetHostileGroup().Contains(behavior.GetGroup()))
        {
            if (behavior.GetShieldState() && behavior.GetOwerFacingDir() == -GetOwerFacingDir())
                behavior.BeatBack();
            else
                behavior.Damage(_owerTf.transform.position, _damageValue);
            var pos = collision.bounds.ClosestPoint(transform.position);
            CreateBulletBounceEffect(pos);
        }
    }
    public void SetActive(bool value)
        => gameObject.SetActive(value);
    public int GetOwerFacingDir()
        => _owerTf.localEulerAngles == Vector3.up * 180f ? -1 : 1;
    //产生特效
    public void CreateBulletBounceEffect(Vector3 pos)
    {
        var effect = PoolManager.Instance.GetFromPool(Paths.PREFAB_EFFECT_BULLETBOUNCE);
        effect.AddOrGet<AutoRecycleComponent>().Init(1f);
        effect.transform.position = pos;
        effect.transform.rotation = _owerTf.transform.rotation;
    }
}
