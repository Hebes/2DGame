/****************************************************
    文件：InputMgr.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/3 12:10:15
	功能：输入系统
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class InputMgr : MonoSingleTon<InputMgr>, IInit
{
    public InputData InputData { get; private set; }
    private bool _isChangingKeyCode;
    private Action<KeyCode> SetKeyAction;
    private Action<KeyCode> DisplayKeyAction;
    public void Init()
    {
        InitInputData();
    }
    private void InitInputData()
        => InputData = ResMgr.Instance.LoadRes<InputData>(Paths.DATA_INPUT);
    public void Update()
    {
        if (InputData == null)
            return;
        InputData.AcceptInput();
        CheckChangeKeyCode();
    }
    private void CheckChangeKeyCode()
    {
        if (!_isChangingKeyCode || !Input.anyKeyDown)
            return;
        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            //不能包含鼠标
            if (keyCode.ToString().Contains("Mouse"))
                continue;
            if (Input.GetKeyDown(keyCode))
            {
                CheckIsSetNone(keyCode);//置空之前的相同的键位
                SetKeyAction?.Invoke(keyCode);
                DisplayKeyAction?.Invoke(keyCode);
                _isChangingKeyCode = false;
                SetKeyAction = null;
                DisplayKeyAction = null;
                EventMgr.Instance.ExecuteEvent(E_EventName.BEGINNERGUIDANCE_RFRESH);
            }
        }
    }
    #region 得到键位信息相关函数
    public bool GetKey(string name)
        => InputData.GetKeyDown(name);
    public bool GetKeyDown(string name)
        => InputData.GetKeyDown(name);
    public bool GetKeyUp(string name)
        => InputData.GetKeyUp(name);
    public bool GetKeyDownTwice(string name)
        => InputData.GetKeyDownTwice(name);
    public float GetValue(string name)
        => InputData.GetValue(name);
    public float GetAxis(string name)
        => InputData.GetAxis(name);
    public float GetAxisRaw(string name)
        => InputData.GetAxisRaw(name);
    #endregion
    #region 设置键位函数
    public void SetKey(string name, KeyCode keyCode)
        => InputData.SetKey(name, keyCode);
    public void SetValueKey(string name, KeyCode keyCode)
        => InputData.SetValueKey(name, keyCode);
    public void SetAxisKey(string name, KeyCode posKey, KeyCode negKey)
        => InputData.SetAxisKey(name, posKey, negKey);
    public void SetAxisPosKey(string name, KeyCode posKey)
        => InputData.SetAxisPosKey(name, posKey);
    public void SetAxisNegKey(string name, KeyCode negKey)
        => InputData.SetAxisNegKey(name, negKey);

    public string GetKeyName(KeyCode code)
        => InputData.GetKeyName(code);
    public void CheckIsSetNone(KeyCode code)
    {
        var name = GetKeyName(code);
        if (name == "")
            return;
        var strs = name.Split('|');
        switch ((E_KeyType)Enum.Parse(typeof(E_KeyType), strs[0]))
        {
            case E_KeyType.Key:
                SetKey(strs[1], KeyCode.None);
                break;
            case E_KeyType.ValueKey:
                SetValueKey(strs[1], KeyCode.None);
                break;
            case E_KeyType.AxisPosKey:
                SetAxisPosKey(strs[1], KeyCode.None);
                break;
            case E_KeyType.AxisNegKey:
                SetAxisNegKey(strs[1], KeyCode.None);
                break;
        }
        EventMgr.Instance.ExecuteEvent(E_EventName.INPUTSYS_SETKEYNONE, name);
    }
    //开始改键位
    public void StartSetKey(Action<KeyCode> setKeyAction, Action<KeyCode> displayKeyAction)
    {
        _isChangingKeyCode = true;
        SetKeyAction = setKeyAction;
        DisplayKeyAction = displayKeyAction;
    }
    #endregion
    //#region 配置读取相关
    //public void LoadInputSetting(string path)
    //    => _inputData.LoadingInputSetting(path);
    //public void SaveInputSetting(string path)
    //    => _inputData.SaveInputSetting(path);
    //#endregion
    //恢复默认输入系统配置
    public void ResetInputSetting()
    {
        var data = ResMgr.Instance.LoadRes<InputData>(Paths.DATA_DEFAULTINPUT);
        //因为是引用类型
        InputData._keys = new List<KeyData>();
        InputData._valueKeys = new List<ValueKeyData>();
        InputData._axisKeys = new List<AxisKeyData>();
        foreach (var key in data._keys)
            InputData._keys.Add(key.ShallowClone());
        foreach (var key in data._valueKeys)
            InputData._valueKeys.Add(key.ShallowClone());
        foreach (var key in data._axisKeys)
            InputData._axisKeys.Add(key.ShallowClone());
    }
    #region 得到键位对应的名字
    public KeyCode GetKeyCode(string name)
    {
        var key = InputData.GetKeyObject(name);
        if (key != null)
            return key.keyCode;
        return KeyCode.None;
    }
    public KeyCode GetValueKey(string name)
    {
        var key = InputData.GetValueKeyObject(name);
        if (key != null)
            return key.keyCode;
        return KeyCode.None;
    }

    public KeyCode GetAxisPosKey(string name)
    {
        var key = InputData.GetAxisKeyObject(name);
        if (key != null)
            return key.posKey;
        return KeyCode.None;
    }
    public KeyCode GetAxisNegKey(string name)
    {
        var key = InputData.GetAxisKeyObject(name);
        if (key != null)
            return key.negKey;
        return KeyCode.None;
    }
    #endregion

}