/****************************************************
    文件：CameraShakeArgs.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/11 13:52:19
	功能：Nothing
*****************************************************/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class CameraShakeArgs 
{
    public float cameraShakeTime;
    public float cameraShakeStrength;
    public float cameraShakeFrequency;
    public Ease cameraShakeEase = Ease.InOutQuart;
}