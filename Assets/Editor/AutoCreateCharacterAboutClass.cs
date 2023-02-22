using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using System.IO;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.Linq;

public class AutoCreateCharacterAboutClass : OdinEditorWindow
{
    //需要导入的命名空间
    private static HashSet<string> _needNameSpcae = new HashSet<string>()
    {
        "System.Collections",
        "System.Collections.Generic",
        "UnityEngine",
    };
    protected static HashSet<string> _rightFolderName = new HashSet<string>
    {
        "Enemy",
        "Hero",
        "Boss"
    };
    [EnumToggleButtons]
    public E_StateName selectState = E_StateName.All;
    [BoxGroup("Info"), ShowInInspector]
    private static string createFolderName = "State";//生成文件夹名称
    [BoxGroup("Info"), ShowInInspector]
    private static string createEnemyName;
    [BoxGroup("Info"), ShowInInspector, HideIf("@curCharacterHeadName==HeroName")]
    private static bool isAirEnemy = false;
    [ShowInInspector, DisableIf("@true")]
    private static string curCharacterHeadName;
    private static string curFolderPath;
    private const string HeroName = "Hero";
    [MenuItem("Assets/CreateCharacterAboutClass")]
    private static void CreateCharacterAboutClass()
    {
        string[] ids = Selection.assetGUIDs;
        CreateCharacterAboutClass(AssetDatabase.GUIDToAssetPath(ids[0]));
    }
    private static void CreateCharacterAboutClass(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            EditorUtility.DisplayDialog("提示", $"该文件夹路径不存在:{folderPath}", "确定");
            return;
        }
        if (!CheckFolderName(folderPath.Substring(folderPath.LastIndexOf('/') + 1)))
        {
            EditorUtility.DisplayDialog("提示", $"该文件不是正确的角色文件夹:{folderPath}", "确定");
            return;
        }
        curFolderPath = folderPath;
        GenerateCreateData(folderPath);
        var window = GetWindow<AutoCreateCharacterAboutClass>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 400);
    }
    [Button("生成", ButtonSizes.Large, ButtonStyle.Box), GUIColor(0.2f, 1f, 0.2f)]
    private void Generate()
    {
        string newFolderPath = curFolderPath + "/" + createFolderName;
        if (!Directory.Exists(newFolderPath))
            Directory.CreateDirectory(newFolderPath);
        List<string> stateName = GetSelectedStateName();
        foreach (var state in stateName)
        {
            string fileName = (isAirEnemy ? "Air" : "") + curCharacterHeadName + createEnemyName + state + "State";
            CreateClass(fileName, newFolderPath);
        }
        AssetDatabase.Refresh();
    }
    private List<string> GetSelectedStateName()
    {
        List<string> stateNameList = new List<string>();
        if (selectState == E_StateName.All)
        {
            foreach (var name in Enum.GetValues(typeof(E_StateName)))
            {
                if ((E_StateName)name == E_StateName.All)
                    continue;
                stateNameList.Add(name.ToString());
            }
        }
        else
            stateNameList = selectState.ToString().Split(',').ToList();
        for (int i = 0; i < stateNameList.Count; i++)
            stateNameList[i] = stateNameList[i].Trim();
        return stateNameList;
    }
    //检测是否为在空中的敌人
    private static void GenerateCreateData(string folderPath)
    {
        var folderName = folderPath.Substring(folderPath.LastIndexOf('/') + 1);
        if (!folderName.Contains("Enemy"))
            isAirEnemy = false;
        else if (folderName.Contains("Air"))
            isAirEnemy = true;
        var strs = folderName.Split('_');
        createEnemyName = strs[strs.Length - 1];
    }
    //生成相关类
    private void CreateClass(string className, string folderPath)
    {
        //准备一个代码编译器单元
        CodeCompileUnit unit = new CodeCompileUnit();
        //准备必要的命名空间（这个是指要生成的类的空间）
        CodeNamespace sampleNamespace = new CodeNamespace();
        //导入必要的命名空间
        foreach (var nameSpace in _needNameSpcae)
            sampleNamespace.Imports.Add(new CodeNamespaceImport(nameSpace));
        //准备要生成的类的定义
        CodeTypeDeclaration Customerclass = new CodeTypeDeclaration(className);
        //指定这是一个Class
        Customerclass.IsClass = true;
        Customerclass.TypeAttributes = TypeAttributes.Public;
        sampleNamespace.Types.Add(Customerclass);
        unit.Namespaces.Add(sampleNamespace);
        //生成代码
        CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        CodeGeneratorOptions options = new CodeGeneratorOptions();
        options.BracingStyle = "C";
        options.BlankLinesBetweenMembers = true;
        using (StreamWriter sw = new StreamWriter(folderPath + "/" + className + ".cs"))
            provider.GenerateCodeFromCompileUnit(unit, sw, options);
    }
    private static bool CheckFolderName(string folderPath)
    {
        foreach (var characterName in _rightFolderName)
        {
            if (folderPath.Contains(characterName))
            {
                curCharacterHeadName = characterName;
                return true;
            }
        }
        return false;
    }
}
[Flags]
public enum E_StateName
{
    Idle = 1 << 1,
    Move = 1 << 2,
    Detected = 1 << 3,
    Charge = 1 << 4,
    Back = 1 << 5,
    LookForPlayer = 1 << 6,
    MeleeAttack = 1 << 7,
    RangeAttack = 1 << 8,
    MeleeAirAttack = 1 << 9,
    RangeAirAttack = 1 << 10,
    InAir = 1 << 11,
    Hit = 1 << 12,
    Dead = 1 << 13,
    Dash = 1 << 14,
    Wait = 1 << 15,
    Shield = 1 << 16,
    Land = 1 << 17,
    Dodge = 1 << 18,
    Appear = 1 << 19,
    Disappear = 1 << 20,
    Rigidity = 1 << 21,
    All = Idle | Move | Detected | Charge | Back | LookForPlayer | MeleeAttack | MeleeAirAttack | RangeAttack
         | RangeAirAttack | InAir | Hit | Dead | Dash | Wait | Shield | Land | Dodge | Appear | Disappear | Rigidity
}
