using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跳转场景后本物体不删除
/// </summary>
public class GameScene : MonoBehaviour
{
    public Texture2D mouseTxt;  //鼠标图标
    private float dt;
    private static bool isLoaded = false;

    private void Awake()
    {
        //这里只调用一次，所以可以防止跳转后一个场景两个
        if (isLoaded)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
            GameApp.Instance.Init();
        }
    }

    private void Start()
    {
        //设置鼠标样式
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);

        //注册配置表
        RegisterConfig();

        GameApp.ConfigManager.LoadAllConfigs(); //加载配置表

        //测试配置表
        ConfigData tempData = GameApp.ConfigManager.GetConfigData("enemy");
        string name = tempData.GetDataById(10001)["Name"];
        Debug.Log(name);

        //背景音乐播放
        GameApp.SoundManager.PlayBGM("login");

        RegisterModule();   //注册游戏中的控制器

        InitModule();
    }

    private void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
        GameApp.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameApp.ControllerManager.Register(ControllerType.Level, new LevelController());
        GameApp.ControllerManager.Register(ControllerType.Fight, new FightController());
    }

    //执行所有控制器初始化
    private void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }

    //注册配置表
    private void RegisterConfig()
    {
        GameApp.ConfigManager.Register("enemy", new ConfigData("enemy"));
        GameApp.ConfigManager.Register("level", new ConfigData("level"));
        GameApp.ConfigManager.Register("option", new ConfigData("option"));
        GameApp.ConfigManager.Register("player", new ConfigData("player"));
        GameApp.ConfigManager.Register("role", new ConfigData("role"));
        GameApp.ConfigManager.Register("skill", new ConfigData("skill"));
        GameApp.ConfigManager.Register("map", new ConfigData("map"));
    }

    private void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}
