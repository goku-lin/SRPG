using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌方信息面板
/// </summary>
public class EnemyDesView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Enemy enemy = (Enemy)args[0];
        Find<Image>("bg/icon").SetIcon(enemy.data["Icon"]);
        Find<Image>("bg/hp/fill").fillAmount = (float)enemy.CurHp / (float)enemy.MaxHp;
        Find<Text>("bg/hp/txt").text = $"{enemy.CurHp}/{enemy.MaxHp}";
        Find<Text>("bg/atkTxt/txt").text = enemy.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = enemy.Step.ToString();
    }
}
