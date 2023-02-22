/****************************************************
    文件：BinaryReader.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/1 16:51:31
	功能：2进制数据读取器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryReader : IReader
{
    private string Path => Paths.BINARYDATA_FOLDER + "{0}.xdx";
    public T LoadData<T>(string fileName) where T : class, new()
    {
        var path = string.Format(Path, fileName);
        if (File.Exists(path))
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var bf = new BinaryFormatter();
                var data = bf.Deserialize(fs) as T;
                return data;
            }
        }
        return default(T);
    }

    public void SaveData(object data, string fileName)
    {
        if(!Directory.Exists(Paths.BINARYDATA_FOLDER))
            Directory.CreateDirectory(Paths.BINARYDATA_FOLDER);
        var path = string.Format(Path, fileName);
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            var bf = new BinaryFormatter();
            bf.Serialize(fs, data);
            fs.Flush();
        }
    }
}