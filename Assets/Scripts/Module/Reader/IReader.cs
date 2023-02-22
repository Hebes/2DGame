/****************************************************
    文件：IReader.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/1 16:17:30
	功能：数据读取接口
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IReader
{
    void SaveData(object data, string fileName);
    T LoadData<T>(string fileName) where T:class,new();
}