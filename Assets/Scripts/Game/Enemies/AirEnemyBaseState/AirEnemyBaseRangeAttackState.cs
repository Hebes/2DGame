using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBaseRangeAttackState<T, X> : AirEnemyAbilityState<T, X> where T : EnemyBase<X> where X : AirEnemyBaseData
{
    protected float _curMovementVelocity;//当前攻击的移动速度
    protected bool _playerInRangeAttack;//主角是否在远程攻击范围内
    protected int _combatIndex = 1;//当前远程攻击的索引
    protected int _curAttackIndex;
    protected float _lastRangeAttackCDTime = float.NegativeInfinity;
    public AirEnemyBaseRangeAttackState(FiniteStateMachine fsm, string animBoolName, T ower, X data) : base(fsm, animBoolName, ower, data)
    {
       
    }
    public override void Enter()
    {
        base.Enter();
        Combat.OpenAttack(E_Attack.RANGE, SetAbilityDown, _combatIndex);
        _curMovementVelocity = Combat.GetCurCombatVelocity();
        Move.SetXVelocityInFacing(_curMovementVelocity, true);
        _curAttackIndex++;
        CheckAttackIsOver();
    }

    public override void Check()
    {
        base.Check();
        _playerInRangeAttack = ColliderCheck.PlayerInRangeAttackDis;
    }
    //生成远程攻击预制体
    protected virtual void SpawnRangeAttack()
    {
        SpawnBullet();
    }
    protected override void SetAbilityDown()
    {
        base.SetAbilityDown();
        SpawnRangeAttack();
    }
    #region 子弹生成相关
    protected virtual void SpawnBullet()
    {
        if (!Combat.GetCombatActive())
            return;
        var prefabGo = GetFromPool();
        var bullet = prefabGo.GetComponent<IBulletBase>();
        //bool isFlip = false;
        //if (Move.FacingDir == -1)
        //    isFlip = true;
        var pos = GetSpawnBulletPos();
        bullet.Init(GetSpawnBulletDir(), Behavior.GetGroup(), Combat.GetHostileGroup(), pos/*, isFlip*/);
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
            //bool isFlip = false;
            //if (Move.FacingDir == -1)
            //    isFlip = true;
            var pos = GetSpawnBulletPos();
            //如果是双数
            if (num % 2 == 0)
                bullet.Init
                    (Quaternion.AngleAxis(angel * (i - mid) + angel / 2, Vector3.forward) * GetSpawnBulletDir()
                    ,
                    Behavior.GetGroup()
                    , Combat.GetHostileGroup()
                    , pos
                    /*, isFlip*/);
            else
                bullet.Init
                    (Quaternion.AngleAxis(angel * (i - mid), Vector3.forward) * GetSpawnBulletDir()
                    , Behavior.GetGroup()
                    , Combat.GetHostileGroup()
                    , pos
                    /*, isFlip*/);
        }
    }
    protected virtual void CreateScreenBulletLeftAndRight(int num, bool isRight = true)
    {
        if (!Combat.GetCombatActive())
            return;
        if (num <= 1)
        {
            Debug.LogError($"生成数量不能小于2个");
            return;
        }
        var screenMinPos = ScreenPosUtil.GetScreenMinPos();
        var screenMaxPos = ScreenPosUtil.GetScreenMaxPos();
        var xPos = isRight ? screenMaxPos.x : screenMinPos.x;
        var yminPos = ScreenPosUtil.GetScreenMinPos().y;
        var ymaxPos = ScreenPosUtil.GetScreenMaxPos().y;
        float offset = (ymaxPos - yminPos) / (num - 1);
        float yMidPos = yminPos + (ymaxPos - yminPos) / 2;
        int midNum = num / 2;
        for (int i = 0; i < num; i++)
        {
            var prefabGo = GetFromPool();
            var bullet = prefabGo.GetComponent<IBulletBase>();
            //bool isFlip = false;
            //if (Move.FacingDir == -1)
            //    isFlip = true;
            Vector2 pos = new Vector2(xPos, 0);
            //单数和双数的计算不同
            pos.y = yMidPos + (i - midNum) * offset + (num % 2 == 0 ? offset / 2 : 0);
            bullet.Init(isRight ? Vector2.left : Vector2.right, Behavior.GetGroup(), Combat.GetHostileGroup(), pos/*, isFlip*/);
        }
    }
    protected virtual void CreateScreenBulletUpAndDown(int num, bool isUp = true)
    {
        if (!Combat.GetCombatActive())
            return;
        if (num <= 1)
        {
            Debug.LogError($"生成数量不能小于2个");
            return;
        }
        var screenMinPos = ScreenPosUtil.GetScreenMinPos();
        var screenMaxPos = ScreenPosUtil.GetScreenMaxPos();
        var yPos = isUp ? screenMaxPos.y : screenMinPos.y;
        var xminPos = ScreenPosUtil.GetScreenMinPos().x;
        var xmaxPos = ScreenPosUtil.GetScreenMaxPos().x;
        float offset = (xmaxPos - xminPos) / (num - 1);
        float xMidPos = xminPos + (xmaxPos - xminPos) / 2;
        int midNum = num / 2;
        for (int i = 0; i < num; i++)
        {

            var prefabGo = GetFromPool();
            var bullet = prefabGo.GetComponent<IBulletBase>();
            Vector2 pos = new Vector2(0, yPos);
            //双数和单数的计算不一样
            pos.x = xMidPos + (i-midNum) * offset + (num % 2 == 0 ? offset / 2 : 0);
            bullet.Init(isUp ? Vector2.down : Vector2.up, Behavior.GetGroup(), Combat.GetHostileGroup(), pos/*, isFlip*/);
        }
    }
    #endregion
    #region 炸弹生成相关
    //生成一屏幕的炸弹
    public virtual void CreateScreenBomb(int num)
    {
        if (!Combat.GetCombatActive() || PlayerTf == null)
            return;
        if (num <= 1)
        {
            Debug.LogError($"生成数量不能小于2个");
            return;
        }
        var screenMinXPos = ScreenPosUtil.GetScreenMinPos().x;
        var screenMaxXPos = ScreenPosUtil.GetScreenMaxPos().x;
        int midNum = num / 2;
        float offset = (screenMaxXPos - screenMinXPos) / (num-1);
        Vector3 playerPos = PlayerTf.position;
        for (int i = 0; i < num; i++)
        {
            //单数和双数的计算不同
            Vector3 pos = GroundPosUtil.GetTargetGroundPos
               (playerPos
               + new Vector3((i - midNum) * offset /*+ (num % 2 == 0 ? offset / 2f : 0)*/, 0, 0));
            //此时代表没有正确的获取到位置
            if(pos==Vector3.zero)
                continue;
            var prefabGo = GetFromPool();
            var bomb = prefabGo.GetComponent<IBombBase>();
            //单数和双数的计算不同
            bomb.Init(pos, GetSpawnBombDir(), Behavior.GetGroup(), Combat.GetHostileGroup());
        }
    }
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
    public void CheckAttackIsOver()
    {
        if (_curAttackIndex >= GetMaxRangeAttackNum())
        {
            _lastRangeAttackCDTime = Time.time;
            _curAttackIndex = 0;
        }
    }
    //得到当前远程攻击的最大连续攻击次数
    public virtual int GetMaxRangeAttackNum()
        => _data.rangeAttackNum;
    //检测是否可以进入远程攻击状态
    public bool CheckCanEnterRangeAttack()
        => Time.time >= _lastRangeAttackCDTime + _data.rangeAttackCoolDownTime;
}
