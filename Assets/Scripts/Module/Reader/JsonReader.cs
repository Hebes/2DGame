/****************************************************
    文件：JsonReader.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/1 16:17:48
	功能：Json读取器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;

public class JsonReader : IReader
{
    private string Path => Paths.JSONDATA_FOLDER + "{0}.json";
    public T LoadData<T>(string fileName) where T : class, new()
    {
        var path = string.Format(Path, fileName);
        if (File.Exists(path))
        {
            var jsonStr = File.ReadAllText(path);
            return JsonMapper.ToObject<T>(jsonStr);
        }
        Debug.LogError($"读取Json数据失败,该路径:{path}不存在");
        return default(T);
    }
    public void SaveData(object data, string fileName)
    {
        if (!Directory.Exists(Paths.JSONDATA_FOLDER))
            Directory.CreateDirectory(Paths.JSONDATA_FOLDER);
        var jsonStr = JsonMapper.ToJson(data);
        var path = string.Format(Path, fileName);
        File.WriteAllText(path, jsonStr);
    }
}