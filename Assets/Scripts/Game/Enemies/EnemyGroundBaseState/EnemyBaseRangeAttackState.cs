using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人远程攻击状态基类
public class EnemyBaseRangeAttackState<T, X> : EnemyAbilityState<T, X> where T : EnemyBase<X> where X : EnemyBaseData
{
    protected float _curMovementVelocity;
    protected bool _isInRangeAttack;
    protected int _combatIndex = 1;//当前远程攻击的索引
    protected float _lastRangeAttackCDTime = float.NegativeInfinity;
    protected int _curAttackIndex;
    public EnemyBaseRangeAttackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {
        //默认远程攻击索引为1
        SetRangeAttackCombatIndex(1);
    }
    public override void Enter()
    {
        base.Enter();
        Combat.OpenAttack(E_Attack.RANGE, SetAbilityDown, _combatIndex);
        _curMovementVelocity = Combat.GetCurCombatVelocity();
        Move.SetXVelocityInFacing(_curMovementVelocity, true);
        Move.LookAtPlayer();
        _curAttackIndex++;
        CheckAttackIsOver();
    }
    public override void Check()
    {
        base.Check();
        _isInRangeAttack = ColliderCheck.PlayerInRangeAttackDis;
    }
    protected virtual void SpawnRangeAttack()
    {
        SpawnBullet();
    }
    #region 子弹生成相关
    protected virtual void SpawnBullet()
    {
        if (!Combat.GetCombatActive())
            return;
        var prefabGo = GetFromPool();
        var bullet = prefabGo.GetComponent<IBulletBase>();
        var pos = GetSpawnBulletPos();
        bullet.Init(GetSpawnBulletDir(), Behavior.GetGroup(), Combat.GetHostileGroup(), pos/*, isFlip*/);
        //bool isFlip = false;
        //if (Move.FacingDir == -1)
        //    isFlip = true;
    }
    protected virtual Vector3 GetSpawnBulletPos()
        => _ower.transform.position
        + new Vector3(_data.createBulletOffset.x * Move.FacingDir, _data.createBulletOffset.y, 0);
    protected virtual Vector3 GetSpawnBulletDir()
        => _ower.transform.right;
    //一次性生成大量扇形子弹
    protected virtual void CreateLargetBulletOfSector(int num, float angel)
    {
        if (!Combat.GetCombatActive())
            return;
        int mid = num / 2;
        for (int i = 0; i < num; i++)
        {
            var prefabGo = GetFromPool();
            var bullet = prefabGo.GetComponent<IBulletBase>();
            ////bool isFlip = false;
            //if (Move.FacingDir == -1)
            //    isFlip = true;
            var pos = GetSpawnBulletPos();
            //如果是双数
            if (num % 2 == 0)
            {
                bullet.Init
                  (Quaternion.AngleAxis(angel * (i - mid) + angel / 2, Vector3.forward) * GetSpawnBulletDir()
                  ,
                  Behavior.GetGroup()
                  , Combat.GetHostileGroup()
                  , pos
                  /*, isFlip*/);
            }
            else
            {
                bullet.Init
                (Quaternion.AngleAxis(angel * (i - mid), Vector3.forward) * GetSpawnBulletDir()
                , Behavior.GetGroup()
                , Combat.GetHostileGroup()
                , pos
                /*, isFlip*/);
            }
        }
    }
    #endregion
    #region 炸弹生成相关
    protected virtual void SpawnBomb()
    {
        if (!Combat.GetCombatActive())
            return;
        var prefabGo = GetFromPool();
        var bomb = prefabGo.GetComponent<IBombBase>();
        var pos = GetSpawnBombPos();
        bomb.Init(pos, GetSpawnBombDir(), Behavior.GetGroup(), Combat.GetHostileGroup());
    }
    protected virtual Vector3 GetSpawnBombPos()
        => _ower.transform.position
        + new Vector3(_data.createBombOffset.x * Move.FacingDir, _data.createBombOffset.y, 0);
    protected virtual Vector3 GetSpawnBombDir()
        => _ower.transform.right;
    #endregion
    protected override void SetAbilityDown()
    {
        base.SetAbilityDown();
        SpawnRangeAttack();
    }
    //实例化远程攻击预制体
    private GameObject GetFromPool()
    {
        var prefab = Combat.GetRangeAttackPrefab();
        return PoolManager.Instance.GetFromPool(prefab);
    }
    //设置远程攻击的索引
    protected void SetRangeAttackCombatIndex(int index)
    {
        if (index <= 0)
        {
            Debug.LogError($"最小攻击索引为1");
            return;
        }
        _combatIndex = index;
    }
    //检车当前是否可以结束攻击状态从而进入其他状态
    private void CheckAttackIsOver()
    {
        if (_curAttackIndex >= _data.rangeAttackNum)
        {
            _lastRangeAttackCDTime = Time.time;
            _curAttackIndex = 0;
        }
    }
    //检测是否可以进入远程攻击状态
    public bool CheckCanEnterRangeAttack()
        => Time.time >= _lastRangeAttackCDTime + _data.rangeAttackCoolDownTime;
}
