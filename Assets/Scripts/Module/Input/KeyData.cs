/****************************************************
    文件：KeyData.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/2 21:32:16
	功能：自定义键位配置数据
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
public enum E_KeyTrigger
{
    Once,
    Double,//双击
    Continuity//持续按下
}
[Serializable]
public class KeyData
{
    [BoxGroup("KeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public string name;
    [BoxGroup("KeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public E_KeyTrigger trigger;
    [BoxGroup("KeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public KeyCode keyCode;
    [BoxGroup("KeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120),ShowIf("@trigger==E_KeyTrigger.Double")]
    public float pressInterval = 1f;
    [HideInInspector]
    public float realInterval;
    [HideInInspector]
    public bool isDown;
    [HideInInspector]
    public bool isDoubleDown;
    [HideInInspector]
    public bool isUp;
    [HideInInspector]
    public bool acceptDoubleDown;//是否进行双击判断
    [HideInInspector]
    public bool enable = true;
    public void SetEnable(bool enable)
        => this.enable = enable;
    public void SetKey(KeyCode code)
        => keyCode = code;
    public KeyData ShallowClone()
        => this.MemberwiseClone() as KeyData;
}
[Serializable]
public class ValueKeyData
{
    [BoxGroup("ValueKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public string name;
    [BoxGroup("ValueKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public Vector2 range = new Vector2(0, 1f);
    [BoxGroup("ValueKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public KeyCode keyCode;
    [BoxGroup("ValueKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public float addSpeed = 3f;
    [HideInInspector]
    public float value;
    [HideInInspector]
    public bool enable = true;
    public void SetEnable(bool enable)
    {
        this.enable = enable;
        this.value = 0f;
    }
    public void SetKey(KeyCode code)
        => keyCode = code;
    public ValueKeyData ShallowClone()
      => this.MemberwiseClone() as ValueKeyData;
}
[Serializable]
public class AxisKeyData
{
    [BoxGroup("AxisKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public string name;
    [BoxGroup("AxisKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public Vector2 range = new Vector2(-1f, 1f);
    [BoxGroup("AxisKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public KeyCode posKey;
    [BoxGroup("AxisKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public KeyCode negKey;
    [BoxGroup("AxisKeySetting"), GUIColor(0, 1f, 1f), LabelWidth(120)]
    public float addSpeed = 3f;
    [HideInInspector]
    public float value;
    [HideInInspector]
    public float rawValue;
    [HideInInspector]
    public bool enable = true;
    public void SetEnable(bool enable)
    {
        this.enable = enable;
        value = 0f;
    }
    public void SetKey(KeyCode posKey,KeyCode negKey)
    {
        SetPosKey(posKey);
        SetNegKey(negKey);
    }
    public void SetPosKey(KeyCode posKey)
        => this.posKey = posKey;
    public void SetNegKey(KeyCode negKey)
        => this.negKey = negKey;
    public AxisKeyData ShallowClone()
   => this.MemberwiseClone() as AxisKeyData;
}

