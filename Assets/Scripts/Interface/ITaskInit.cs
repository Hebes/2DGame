/****************************************************
    文件：ITask.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/9 19:55:56
	功能：任务接口
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ITaskInit
{
    void Init(Action callback);
}