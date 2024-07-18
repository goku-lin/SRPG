using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄信息面板
/// </summary>
public class HeroDesView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Hero hero = args[0] as Hero;
        Find<Image>("bg/icon").SetIcon(hero.data["Icon"]);
        Find<Image>("bg/hp/fill").fillAmount = (float)hero.CurHp / (float)hero.MaxHp;
        Find<Text>("bg/hp/txt").text = $"{hero.CurHp}/{hero.MaxHp}";
        Find<Text>("bg/atkTxt/txt").text = hero.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = hero.Step.ToString();
    }
}
