/****************************************************
    文件：InpuData.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/2 21:59:59
	功能：输入配置数据集合
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using System;
using LitJson;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[InlineEditor]
[CreateAssetMenu(fileName = "InputData", menuName = "数据/输入系统/InputData")]
public class InputData : ScriptableObject
{
    //不给默认数据的话生成Json数据的时候可能会报错
    public List<KeyData> _keys = new List<KeyData>() { new KeyData() };
    public List<ValueKeyData> _valueKeys = new List<ValueKeyData>() { new ValueKeyData() };
    public List<AxisKeyData> _axisKeys = new List<AxisKeyData>() { new AxisKeyData() };
    public KeyData GetKeyObject(string name)
        => _keys.Find(key => key.name == name);
    public ValueKeyData GetValueKeyObject(string name)
       => _valueKeys.Find(key => key.name == name);
    public AxisKeyData GetAxisKeyObject(string name)
       => _axisKeys.Find(key => key.name == name);
    private KeyData _tempKey;
    private ValueKeyData _tempValueKey;
    private AxisKeyData _tempAxisKey;
    #region 设置键位函数
    public void SetKey(string name, KeyCode keyCode)
    {
        var key = GetKeyObject(name);
        key?.SetKey(keyCode);
    }
    public void SetValueKey(string name, KeyCode keyCode)
    {
        var key = GetValueKeyObject(name);
        key?.SetKey(keyCode);
    }
    public void SetAxisKey(string name, KeyCode posKey, KeyCode negKey)
    {
        var key = GetAxisKeyObject(name);
        key?.SetKey(posKey, negKey);
    }
    public void SetAxisPosKey(string name, KeyCode posKey)
    {
        var key = GetAxisKeyObject(name);
        key?.SetPosKey(posKey);
    }
    public void SetAxisNegKey(string name, KeyCode negKey)
    {
        var key = GetAxisKeyObject(name);
        key?.SetNegKey(negKey);
    }
    #endregion



    #region 得到键位值函数
    public bool GetKeyDown(string name)
    {
        var key = GetKeyObject(name);
        if (key != null)
            return key.isDown;
        return false;
    }
    public bool GetKeyUp(string name)
    {
        var key = GetKeyObject(name);
        if (key != null)
            return key.isUp;
        return false;
    }
    public bool GetKeyDownTwice(string name)
    {
        var key = GetKeyObject(name);
        if (key != null)
            return key.isDoubleDown;
        return false;
    }
    public float GetValue(string name)
    {
        var key = GetValueKeyObject(name);
        if (key != null)
            return key.value;
        return 0f;
    }
    public float GetAxis(string name)
    {
        var key = GetAxisKeyObject(name);
        if (key != null)
            return key.value;
        return 0f;
    }
    public float GetAxisRaw(string name)
    {
        var key = GetAxisKeyObject(name);
        if (key != null)
            return key.rawValue;
        return 0f;
    }
    #endregion
    #region 设置键位启用状态函数
    public void SetKeyEnable(string name, bool enable)
    {
        var key = GetKeyObject(name);
        key?.SetEnable(enable);
    }
    public void SetValueKeyEnable(string name, bool enable)
    {
        var key = GetValueKeyObject(name);
        key?.SetEnable(enable);
    }
    public void SetAxisKeyEnable(string name, bool enable)
    {
        var key = GetAxisKeyObject(name);
        key?.SetEnable(enable);
    }
    #endregion
    #region 帧更新函数
    public void AcceptInput()
    {
        UpdateKeys();
        UpdateValueKeys();
        UpdateAxisKeys();
    }
    private void UpdateKeys()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            _tempKey = _keys[i];
            if (!_tempKey.enable)
                continue;
            _tempKey.isDown = false;
            _tempKey.isDoubleDown = false;
            _tempKey.isUp = false;
            switch (_tempKey.trigger)
            {
                case E_KeyTrigger.Once:
                    if (Input.GetKeyDown(_tempKey.keyCode))
                        _tempKey.isDown = true;
                    if (Input.GetKeyUp(_tempKey.keyCode))
                        _tempKey.isUp = true;
                    break;
                case E_KeyTrigger.Double:
                    if (_tempKey.acceptDoubleDown)
                    {
                        _tempKey.realInterval += Time.deltaTime;
                        if (_tempKey.realInterval > _tempKey.pressInterval)
                        {
                            _tempKey.acceptDoubleDown = false;
                            _tempKey.realInterval = 0f;
                        }
                        else if (Input.GetKeyDown(_tempKey.keyCode))
                        {
                            _tempKey.isDoubleDown = true;
                            _tempKey.realInterval = 0f;
                        }
                        else if (Input.GetKeyUp(_tempKey.keyCode))
                            _tempKey.acceptDoubleDown = false;
                    }
                    else
                    {
                        if (Input.GetKeyUp(_tempKey.keyCode))
                        {
                            _tempKey.acceptDoubleDown = true;
                            _tempKey.realInterval = 0f;
                        }
                    }
                    break;
                case E_KeyTrigger.Continuity:
                    if (Input.GetKey(_tempKey.keyCode))
                        _tempKey.isDown = true;
                    break;
            }
        }
    }
    private void UpdateValueKeys()
    {
        for (int i = 0; i < _valueKeys.Count; i++)
        {
            _tempValueKey = _valueKeys[i];
            if (!_tempValueKey.enable)
                return;
            if (Input.GetKey(_tempValueKey.keyCode))
                _tempValueKey.value = Mathf.Clamp(_tempValueKey.value + Time.deltaTime * _tempValueKey.addSpeed, _tempValueKey.range.x, _tempValueKey.range.y);
            else
                _tempValueKey.value = Mathf.Clamp(_tempValueKey.value - Time.deltaTime * _tempValueKey.addSpeed, _tempValueKey.range.x, _tempValueKey.range.y);
        }
    }
    private void UpdateAxisKeys()
    {
        for (int i = 0; i < _axisKeys.Count; i++)
        {
            _tempAxisKey = _axisKeys[i];
            if (!_tempAxisKey.enable)
                return;
            if (Input.GetKey(_tempAxisKey.posKey))
            {
                _tempAxisKey.rawValue = _tempAxisKey.range.y;
                _tempAxisKey.value = Mathf.Clamp(_tempAxisKey.value += Time.deltaTime * _tempAxisKey.addSpeed, _tempAxisKey.range.x, _tempAxisKey.range.y);
            }
            else if (Input.GetKey(_tempAxisKey.negKey))
            {
                _tempAxisKey.rawValue = _tempAxisKey.range.x;
                _tempAxisKey.value = Mathf.Clamp(_tempAxisKey.value -= Time.deltaTime * _tempAxisKey.addSpeed, _tempAxisKey.range.x, _tempAxisKey.range.y);
            }
            else
            {
                _tempAxisKey.rawValue = 0f;
                _tempAxisKey.value = Mathf.Lerp(_tempAxisKey.value, 0f, Time.deltaTime * _tempAxisKey.addSpeed);
                if (Mathf.Abs(_tempValueKey.value) <= 0.01f)
                    _tempValueKey.value = 0f;
            }
        }
    }
    #endregion
    //#region 存储和读取配置文件
    //public void SaveInputSetting(string path)
    //{
    //    JsonData jsonData = new JsonData();
    //    //存储名字对应的当前键位
    //    jsonData["Keys"] = new JsonData();
    //    foreach (var key in _keys)
    //        jsonData["Keys"][key.name] = key.keyCode.ToString();
    //    jsonData["ValueKeys"] = new JsonData();
    //    foreach (var valueKey in _valueKeys)
    //        jsonData["ValueKeys"][valueKey.name] = valueKey.keyCode.ToString();
    //    jsonData["AxisKeys"] = new JsonData();
    //    foreach (var axisKey in _axisKeys)
    //    {
    //        jsonData["AxisKeys"][axisKey.name] = new JsonData();
    //        jsonData["AxisKeys"][axisKey.name]["Pos"] = axisKey.posKey.ToString();
    //        jsonData["AxisKeys"][axisKey.name]["Neg"] = axisKey.negKey.ToString();
    //    }
    //    File.WriteAllText(path, jsonData.ToJson());
    //}
    //public void LoadingInputSetting(string path)
    //{
    //    if (!File.Exists(path))
    //    {
    //        Debug.LogError($"读取输入系统配置信息失败,该路径不存在{path}");
    //        return;
    //    }
    //    var jsonStr = File.ReadAllText(path);
    //    var jsonData = JsonMapper.ToObject(jsonStr);
    //    foreach (var key in _keys)
    //        key.keyCode = String2Enum(jsonData["Keys"][key.name].ToString());
    //    foreach (var valueKey in _valueKeys)
    //        valueKey.keyCode = String2Enum(jsonData["ValueKeys"][valueKey.name].ToString());
    //    foreach (var axisKey in _axisKeys)
    //    {
    //        axisKey.posKey = String2Enum(jsonData["AxisKeys"][axisKey.name]["Pos"].ToString());
    //        axisKey.negKey = String2Enum(jsonData["AxisKeys"][axisKey.name]["Neg"].ToString());
    //    }
    //}
    //public KeyCode String2Enum(string keyCode)
    //    => (KeyCode)Enum.Parse(typeof(KeyCode), keyCode);
    //#endregion
    public string GetKeyName(KeyCode code)
    {
        foreach (var key in _keys)
            if (code == key.keyCode)
                return "Key|"+key.name;
        foreach (var valueKey in _valueKeys)
            if (code == valueKey.keyCode)
                return "ValueKey|"+valueKey.name;
        foreach (var axisKey in _axisKeys)
        {
            if (code == axisKey.posKey)
                return "AxisPosKey|" + axisKey.name;
            else if (code == axisKey.negKey)
                return "AxisNegKey|" + axisKey.name;
        }
        return "";
    }
}