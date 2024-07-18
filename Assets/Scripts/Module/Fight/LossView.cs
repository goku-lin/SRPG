using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LossView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("okBtn").onClick.AddListener(() =>
        {
            //卸载战斗中的资源
            GameApp.FightManager.ReLoadRes();
            GameApp.ViewManager.CloseAll();

            //切换场景
            LoadingModel load = new LoadingModel();
            load.SceneName = "map";
            load.callBack = () =>
            {
                GameApp.SoundManager.PlayBGM("mapbgm");
                GameApp.ViewManager.Open(ViewType.SelectLevelView);
            };
            Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, load);
        });
    }
}
