using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常量类
/// </summary>
public class Defines
{
    //控制器相关事件字符串
    public static readonly string OpenStartView = "OpenStartView";  //打开开始面板
    public static readonly string OpenSetView = "OpenSetView";  //打开设置面板
    public static readonly string OpenMessageView = "OpenMessageView";  //打开信息面板
    public static readonly string OpenSelectLevelView = "OpenSelectLevelView";  //
    public static readonly string LoadingScene = "LoadingView";  //加载场景
    public static readonly string BeginFight = "BeginFight";

    //全局事件相关
    public static readonly string ShowLevelDesEvent = "ShowLevelDesEvent";
    public static readonly string HideLevelDesEvent = "HideLevelDesEvent";

    public static readonly string OnSelectEvent = "OnSelectEvent";  //选中事件
    public static readonly string OnUnSelectEvent = "OnUnSelectEvent";  //未选中事件

    //option
    public static readonly string OnAttackEvent = "OnAttackEvent";
    public static readonly string OnIdleEvent = "OnIdleEvent";
    public static readonly string OnCancelEvent = "OnCancelEvent";
    public static readonly string OnRemoveHeroToSceneEvent = "OnRemoveHeroToSceneEvent";
}
