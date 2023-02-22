using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

public class GameDataSettingTool : OdinMenuEditorWindow
{
    [MenuItem("MyTools/GameDataSettingTool")]
    private static void ShowWindow()
    {
        var window = GetWindow<GameDataSettingTool>();
        window.position= GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.AddAllAssetsAtPath(Consts.NAME_BULLETDATA,Paths.GAMEDATA_FLODER,typeof(BulletBaseData),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_BOMBDATA,Paths.GAMEDATA_FLODER,typeof(BombBaseData),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_DESTRUCTIBLEITEMDATA,Paths.GAMEDATA_FLODER,typeof(DestructibleItemBaseData),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_COMBATDATA,Paths.GAMEDATA_FLODER,typeof(CombatData),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_ENEMYDATA,Paths.GAMEDATA_FLODER,typeof(EnemyBaseData),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_HEROMANDATA,Paths.GAMEDATA_FLODER,typeof(HeroManData),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_ITEMDATA, Paths.RESOURCES_FOLDER, typeof(Items),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_SHOPDATA, Paths.RESOURCES_FOLDER, typeof(ShopDatas),true,true);
        tree.AddAllAssetsAtPath(Consts.NAME_NPCDATA, Paths.RESOURCES_FOLDER, typeof(NPCData), true, true);
        return tree;
    }
}
