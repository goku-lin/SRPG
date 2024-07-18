using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理器（存储玩家基本的游戏信息
/// </summary>
public class GameDataManager
{
    public List<int> heros; //英雄合集

    public int Money;   //金币？

    public GameDataManager()
    {
        heros = new List<int>();

        //默认三个英雄，预先存起来
        heros.Add(10001);
        heros.Add(10002);
        heros.Add(10003);
    }
}
