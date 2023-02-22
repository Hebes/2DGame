/****************************************************
    文件：InputSystemSettingTool.cs
	作者：桔梗
    邮箱: 1784603269@qq.com
    日期：2021/12/3 14:25:37
	功能：输入系统配置工具
*****************************************************/
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class InputSystemSettingTool : OdinMenuEditorWindow
{
    private const string InputSetting = "InputSetting";
    [MenuItem("MyTools/InputSystemSettingTool")]
    private static void ShowWindow()
    {
        var window = GetWindow<InputSystemSettingTool>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.AddAllAssetsAtPath(InputSetting, Paths.RESOURCES_FOLDER, typeof(InputData), true, true);
        return tree;
    }
}