using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public static class ExtendUtil
{
    private const string ColorRed = "<color=#FF0000FF>";
    private const string ColorGreen = "<color=#00FF00FF>";
    private const string ColorBlue = "<color=#00B4FFFF>";
    private const string ColorYellow = "<color=#FFFF00FF>";
    private const string ColorEnd = "</color>";

    public static T Add<T>(this Transform trans) where T : Component 
        => trans.gameObject.AddComponent<T>();
    public static T AddOrGet<T>(this GameObject gameObject) where T : Component
    {
        var temp = gameObject.GetComponent<T>();
        if (temp == null)
            temp = gameObject.AddComponent<T>();
        return temp;
    }
    public static T AddOrGet<T>(this Transform trans) where T : Component
    {
        var temp = trans.gameObject.GetComponent<T>();
        if (temp == null)
            temp = trans.gameObject.AddComponent<T>();
        return temp;
    }
    public static float GetAnmLength(this Animator anim, string name)
    {
        float length = 0f;
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            //注意字符串最好通过这样比较
            if (clip.name.Equals(name))
            {
                length = clip.length;
                break;
            }
        }
        return length;
    }
    //很傻逼的方法 
    public static void SetAnimatorAgrs(this Animator anim, AnimatorControllerParameterType type, string name, object arg)
    {
        switch (type)
        {
            case AnimatorControllerParameterType.Float:
                anim.SetFloat(name, (float)arg);
                break;
            case AnimatorControllerParameterType.Int:
                anim.SetInteger(name, (int)arg);
                break;
            case AnimatorControllerParameterType.Bool:
                anim.SetBool(name, (bool)arg);
                break;
            case AnimatorControllerParameterType.Trigger:
                anim.SetTrigger(name);
                break;
        }
    }


    public static Vector2 Reverse(this Vector2 vector)
       => new Vector2(-vector.x, -vector.y);
    public static Vector2 ReverseX(this Vector2 vector)
       => new Vector2(-vector.x, vector.y);
    public static Vector2 ReverseY(this Vector2 vector)
       => new Vector2(vector.x, -vector.y);

    #region UI相关工具函数
    public static void SetText(this Text text, string msg="")
       => text.text = msg;
    public static void SetText(this Text text, int msg=0)
        => text.SetText(msg.ToString());
    public static void SetText(this Text text, float msg=0)
        => text.SetText(msg.ToString());
    public static void SetImage(this Image img, Sprite sprite)
        => img.sprite = sprite;
    public static void SetValue(this Slider sld, float value)
        => sld.value = value;
    public static void SetEvent(this Button btn, Action callback)
        => btn.onClick.AddListener(()=>callback());
    public static void SetActive(this UIBehaviour behaviour, bool value)
        => behaviour.gameObject.SetActive(value);
    public static void SetAnchorPos(this UIBehaviour behaviour, Vector2 pos)
        => behaviour.transform.Rect().anchoredPosition = pos;
    public static Vector2 GetAnchorPos(this UIBehaviour behaviour)
        => behaviour.transform.Rect().anchoredPosition;
    public static RectTransform Rect(this Transform trans)
        => trans.GetComponent<RectTransform>();
    //快速设置富文本格式的文本
    public static string SetColor(this string str,E_TextColor color)
    {
        string result = "";
        switch (color)  
        {
            case E_TextColor.Red:
                result = $"{ColorRed}";
                break;
            case E_TextColor.Green:
                result = $"{ColorGreen}";
                break;
            case E_TextColor.Blue:
                result = $"{ColorBlue}";
                break;
            case E_TextColor.Yellow:
                result = $"{ColorYellow}";
                break;
        }
        result+=$"{str}{ColorEnd}";
        return result;
    }
    public static T Add<T>(this UIBehaviour behavior) where T:Component
        => behavior.gameObject.AddComponent<T>();
    public static T AddOrGet<T>(this UIBehaviour behavior) where T : Component
         => behavior.gameObject.AddOrGet<T>();
    #endregion
    public static void SetActive(this Transform trans, bool value)
        => trans.gameObject.SetActive(value);
    public static void SetParent(this GameObject go, Transform parent)
        => go.transform.SetParent(parent);
}
public enum E_TextColor
{
    Red,
    Green,
    Blue,
    Yellow
}

