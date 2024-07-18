using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗结束
/// </summary>
public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();

        GameApp.CommandManager.Clear(); //清楚指令

        //这里本来胜利界面和失败界面想做在一起的
        if (GameApp.FightManager.heros.Count == 0)
        {
            //延迟一点时间才出现界面
            GameApp.CommandManager.AddCommand(new WaitCommand(1.2f, () =>
            {
                GameApp.ViewManager.Open(ViewType.LossView);
            }));
        }
        else if (GameApp.FightManager.enemys.Count == 0)
        {
            GameApp.CommandManager.AddCommand(new WaitCommand(1.2f, () =>
            {
                GameApp.ViewManager.Open(ViewType.WinView);
            }));
        }
        else
        {

        }
    }

    public override bool Update(float dt)
    {
        return true;
    }
}
