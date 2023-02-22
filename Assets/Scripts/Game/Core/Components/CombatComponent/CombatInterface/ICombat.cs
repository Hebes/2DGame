using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat 
{
    HashSet<E_Group> GetHostileGroup();//得到敌对阵营
    void SetHostileGroupHash(HashSet<E_Group> hostileGroup);//设置敌对阵营
    int GetOwerFacingDir();
}
