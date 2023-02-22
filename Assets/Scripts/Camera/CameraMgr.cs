using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;
using System.Threading.Tasks;
/// <summary>
/// 游戏场景中的相机管理器 管理游戏场景中相机的变化
/// </summary>
public class CameraMgr : MonoDestroySingleTon<CameraMgr>,ITaskInit
{
    private CinemachineBrain _brainCamera;//虚拟摄像机控制器 控制场景中所有虚拟摄像机
    private CinemachineVirtualCamera _curVirtualCamera;//当前使用虚拟相机
    private CinemachineBasicMultiChannelPerlin _cameraShakeComponent;
    private Tween _curTween;
    public async void Init(Action callback)
    {
        _brainCamera = gameObject.AddOrGet<CinemachineBrain>();
        UpdateCurVirtualCamera();
        await Task.Delay(TimeSpan.FromSeconds(1f));
        var playerTf = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        SetCurVirtualCameraTarget(playerTf);
        StopAllShake();
        await Task.Delay(TimeSpan.FromSeconds(1f));
        callback?.Invoke();
    }
    //更新当前虚拟相机
    public void UpdateCurVirtualCamera()
    {
        _curVirtualCamera = _brainCamera.ActiveVirtualCamera as CinemachineVirtualCamera;
        //这里需要使用自带的获取组件的方法
        _cameraShakeComponent = _curVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void SetCurVirtualCameraTarget(Transform targetTf)
        => _curVirtualCamera.Follow = targetTf;
    //相机的普通振动  现在振动的配置文件不能修改
    public void DoCameraNormalShake(float duration,float strength,float frequency,Ease ease=Ease.InOutQuart)
    {
        StopAllShake();
        _cameraShakeComponent.m_AmplitudeGain = strength;
        _cameraShakeComponent.m_FrequencyGain = frequency;
       _curTween=DOTween.To(() => _cameraShakeComponent.m_AmplitudeGain, (value) => _cameraShakeComponent.m_AmplitudeGain = value, 0, duration)
            .SetAutoKill(true)
            .SetUpdate(true)
            .SetEase(ease)
            .OnKill(()=>_curTween=null);
    }
    //停止所有震动
    public void StopAllShake()
    {
        _cameraShakeComponent.m_AmplitudeGain = 0;
        _cameraShakeComponent.m_FrequencyGain = 0;
        if (_curTween == null)
            return;
        if (_curTween.IsPlaying())
            _curTween.Kill();
        _curTween = null;
    }
   
}
