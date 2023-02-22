using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior 
{
    void Damage(Vector3 attackPos, float damageValue,E_Attack type=E_Attack.NONE);//受伤
    //void Knockback(float strength, Vector2 dir, int xDir);
    void Dead();//死亡    
    E_Group GetGroup();//得到自己属于阵营
    bool GetShieldState();
    int GetOwerFacingDir();
    void BeatBack();
}
