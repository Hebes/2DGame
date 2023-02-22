using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System.IO;
[InitializeOnLoad]
public static class AutoAddTags
{
    static AutoAddTags()
    {
        SetUnityTags();//设置Unity的标签
        SetPrefabsTags();//设置预制体的标签
    }
    //设置预制体标签
    private static void SetPrefabsTags()
    {
        SetFloderTags(Paths.PREFAB_ENEMY_FLODER,Tags.Enemy);
        SetFloderTags(Paths.PREFAB_BULLET_FLODER,Tags.Bullet);
        SetFloderTags(Paths.PREFAB_HERO_FLODER,Tags.Player);
        SetFloderTags(Paths.PREFAB_EFFECT_FLODER,Tags.Effect);
        SetFloderTags(Paths.PREFAB_DESTRUCTIBLEITEM_FLODER, Tags.DestructibleItem);
    }
    private static void SetFloderTags(string path,string tag)
    {
        var prefabs = Resources.LoadAll<GameObject>(path);
        foreach (var prefab in prefabs)
            prefab.tag = tag;
    }
    //设置预制体标签
    private static void SetUnityTags()
    {
        var type = typeof(Tags);
        var infos = type.GetFields();
        foreach (var info in infos)
            InternalEditorUtility.AddTag(info.GetRawConstantValue().ToString());
    }
}
