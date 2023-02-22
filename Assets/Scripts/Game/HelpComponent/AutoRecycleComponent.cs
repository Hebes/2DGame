using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRecycleComponent : MonoBehaviour
{
    public void Init(float recycleTime)
    {
        Invoke("Recycle",recycleTime);
    }
    private void Recycle()
    {
        PoolManager.Instance?.PushPool(gameObject);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
}
