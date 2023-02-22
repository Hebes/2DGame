using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletBase
{
    void Init(Vector2 dir, E_Group selfGroup, HashSet<E_Group> hostilityGroup, Vector3 startPos/*, bool isFlip = false*/);
}
