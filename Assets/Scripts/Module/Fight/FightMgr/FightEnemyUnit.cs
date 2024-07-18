using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人回合
/// </summary>
public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.FightManager.ResetHeros();  //重置英雄行动
        GameApp.ViewManager.Open(ViewType.TipView, "敌人回合");

        //等待的时间要稍微延长一些，太短会出现问题，无法回合切换，我认为因为tip动画需要时间
        GameApp.CommandManager.AddCommand(new WaitCommand(1.25f));

        //敌人行动 使用技能等
        for (int i = 0; i < GameApp.FightManager.enemys.Count; i++)
        {
            Hero enemy = GameApp.FightManager.enemys[i];
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));  //等待
            GameApp.CommandManager.AddCommand(new AIMoveCommand(enemy));    //移动
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));  //等待
            GameApp.CommandManager.AddCommand(new SkillCommand(enemy)); //使用技能
            GameApp.CommandManager.AddCommand(new WaitCommand(0.25f));  //等待
        }

        //等待一段时间 切换玩家回合
        GameApp.CommandManager.AddCommand(new WaitCommand(0.2f, () =>
        {
            GameApp.FightManager.ChangeState(GameState.Player);
        }));
    }
}
