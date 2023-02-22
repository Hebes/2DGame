using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class TimeScaleMgr : MonoSingleTon<TimeScaleMgr>
{
    private float _targetBulletScale = 0f;
    private float _tempTimer;//用于插值的计时器
    private float _defalutFixedDeltaTime;//默认的物理帧间隔时间
    private const string OpenBulletTimeName = "OpenBulletTime";
    private int _curCorotineId;//当前开启的协程id
    protected override void Awake()
    {
        base.Awake();
        _defalutFixedDeltaTime = Time.fixedDeltaTime;
    }
    //关闭所有子弹时间
    private void StopBulletTime()
    {
        Time.timeScale = 1;
        //如果有当前开启了关于子弹时间的协程 则关闭该协程
        if (CoroutineMgr.Instance.HasId(_curCorotineId))
            CoroutineMgr.Instance.Stop(_curCorotineId);
        var tweenList = DOTween.TweensById(OpenBulletTimeName);
        if (tweenList == null || tweenList.Count == 0)
            return;
        foreach (var tween in tweenList)
            if (tween.IsPlaying())//如果动画正在播放
                DOTween.Kill(tween);
    }
    //没有缓入换出的子弹时间
    public void OpenBulletTime(float duration)
    {
        StopBulletTime();
        Time.timeScale = _targetBulletScale;
        _curCorotineId = CoroutineMgr.Instance.ExcuteOne(OpenBulletTimeRoutine(duration));
    }
    private IEnumerator OpenBulletTimeRoutine(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
    public void OpenSlowOutBulletTime(float duration, float slowOutTime = 0f,Action callback=null, Ease ease = Ease.InQuad)
    {
        StopBulletTime();//暂停当前所有的子弹时间
        Time.timeScale = _targetBulletScale;
        _curCorotineId = CoroutineMgr.Instance.ExcuteOne(OpenSlowOutBulletRoutine(duration, slowOutTime,callback, ease));
    }
    private IEnumerator OpenSlowOutBulletRoutine(float duration, float slowOutTime,Action callback, Ease ease)
    {
        //等待指定的子弹时间后
        yield return new WaitForSecondsRealtime(duration);
        var tween = GetSetTimeScaleTween(1,slowOutTime,ease);
        yield return tween.WaitForCompletion();
        Time.timeScale = 1;
        yield return null;
        callback?.Invoke();
    }



    //开启缓入缓出的子弹时间
    public void OpenSlowInSlowOutBulleTime(float duration, float slowInTime, float slowOutTime,Action callback=null, Ease slowInEase = Ease.InQuad, Ease slowOutEase = Ease.InQuad)
    {
        StopBulletTime();//暂停当前所有的子弹时间
        _curCorotineId = CoroutineMgr.Instance.ExcuteOne(OpenSlowIntSlowOutBulletTimeRoutine(duration, slowInTime, slowInTime,callback, slowInEase, slowOutEase));
    }
    private IEnumerator OpenSlowIntSlowOutBulletTimeRoutine(float duration, float slowInTime, float slowOutTime,Action callback=null, Ease slowInEase = Ease.InQuad, Ease slowOutEase = Ease.InQuad)
    {
        var tween = GetSetTimeScaleTween(_targetBulletScale, duration, slowInEase);
        yield return tween.WaitForCompletion();//等待缓入动画播放完毕
        Time.timeScale = _targetBulletScale;
        yield return new WaitForSecondsRealtime(duration);
        tween = GetSetTimeScaleTween(1, duration, slowOutEase);
        yield return tween.WaitForCompletion();//等待缓入动画播放完毕
        Time.timeScale = 1;
        yield return null;
        callback?.Invoke();
    }
    //得到设置时间缩放的动画
    private Tween GetSetTimeScaleTween(float targetTimeScale, float duration, Ease ease)
    {
        var tween = DOTween.To(() => Time.timeScale, (value) => Time.timeScale = value, targetTimeScale, duration)
         .SetId(OpenBulletTimeName)
         .SetAutoKill(true)
         .SetUpdate(true) //不受到时间缩放的影响
         .SetEase(ease)
         .OnUpdate(SetTimeFiexedDeltaTime);//改变物理帧间隔的时间
        return tween;
    }
    private void SetTimeFiexedDeltaTime()
        => Time.fixedDeltaTime = _defalutFixedDeltaTime * Time.timeScale;
    //暂停游戏
    public void StopGame()
        => Time.timeScale = 0;
    //继续游戏
    public void ContinueGame()
        => Time.timeScale = 1;
}
