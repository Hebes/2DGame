/****************************************************
    文件：IBombBase.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/11/5 11:43:19
	功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBombBase  
{
    void Init(Vector3 pos, Vector2 dir, E_Group selfGroup, HashSet<E_Group> hostilityGroup);
}