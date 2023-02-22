using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBulletBehavior 
{
    void Parry(E_Group group,HashSet<E_Group> hostilityGroup);//弹反
    void SetGroup(E_Group selfGroup);
    void RecoverHealth();
}
