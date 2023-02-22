/****************************************************
    文件：CreateAudioCfgData.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/25 18:58:24
	功能：生成音频配置数据文件
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using LitJson;
public static class CreateAudioCfgData
{
    private const string fileName = "AudioCfgData";

    [MenuItem("Assets/创建音频数据文件")]
    private static void CreateCfgData()
    {
        string[] ids = Selection.assetGUIDs;
        string path = AssetDatabase.GUIDToAssetPath(ids[0]);
        Create(path);
    }
    private static void Create(string folderPath)
    {
        if (!Directory.Exists(folderPath) || !folderPath.Contains("Audio"))
        {
            EditorUtility.DisplayDialog("提示", "当前文件不存在或者当前文件夹不是音频文件夹", "确定");
            return;
        }
        DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
        FileInfo[] infos = folderInfo.GetFiles("*", SearchOption.AllDirectories);
        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(Application.streamingAssetsPath);
        string cfgFolderPath = Application.streamingAssetsPath + "/Cfg";
        if (!Directory.Exists(cfgFolderPath))
            Directory.CreateDirectory(cfgFolderPath);
        string filePath = cfgFolderPath + $"/{fileName}.json";
        List<AudioCfgData> dataList = new List<AudioCfgData>();
        if (File.Exists(filePath))
            dataList = JsonMapper.ToObject<List<AudioCfgData>>(File.ReadAllText(filePath));
        if (dataList == null)
            dataList = new List<AudioCfgData>();
        string name = "";
        foreach (var info in infos)
        {
            if (info.Extension == ".meta")
                continue;
            name = Path.GetFileNameWithoutExtension(info.Name);
            if (IsContainKey(dataList, name))
                continue;
            dataList.Add(new AudioCfgData
            {
                name=name,
                volume=1
            });
        }
        CreateJsonData(filePath,dataList);
    }
    //判断是否已经存在该音频的配置文件
    private static bool IsContainKey(List<AudioCfgData> list,string name)
    {
        if (list == null||list.Count==0)
            return false;
        foreach (var data in list)
            if (data.name == name)
                return true;
        return false;
    }
    //创建Json数据
    private static void CreateJsonData(string filePath, List<AudioCfgData> audioDatas)
    {
        string jsonData = JsonMapper.ToJson(audioDatas);
        if (File.Exists(filePath))
        {
            if (!EditorUtility.DisplayDialog("提示", "该音频配置文件已经存在,是否覆盖?", "确定", "取消"))
                return;
        }
        File.WriteAllText(filePath, jsonData);
        EditorUtility.DisplayDialog("提示", "音频配置文件创建成功!", "确定");
        AssetDatabase.Refresh();
    }
}
[Serializable]
public class AudioCfgData
{
    public string name;
    public double volume;
}