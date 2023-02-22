using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyComponent : MonoBehaviour
{
    public void Init(float destroyTime)
    {
        Invoke("Function", destroyTime);
    }
    private void Function()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
}
