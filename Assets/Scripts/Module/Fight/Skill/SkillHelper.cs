using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能帮助类
/// </summary>
public static class SkillHelper
{
    /// <summary>
    /// 目标是否在技能范围内
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool IsModelInSkillArea(this ISkill skill, ModelBase target)
    {
        ModelBase current = (ModelBase)skill;
        if (current.GetDis(target) <= skill.skillPro.AttackRange)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 获得技能的作用目标
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public static List<ModelBase> GetTarget(this ISkill skill, ModelBase attacker)
    {
        //0:以鼠标指向的目标为目标
        //1:在攻击范围内的所有目标
        //2.在攻击范围内的英雄的目标
        switch (skill.skillPro.Target)
        {
            case 0:
                return GetTarget_0(skill, attacker);
            case 1:
                return GetTarget_1(skill, attacker); 
            case 2:
                return GetTarget_2(skill, attacker);
        }

        return null;
    }

    //0:以鼠标指向的目标为目标
    public static List<ModelBase> GetTarget_0(ISkill skill, ModelBase attacker)
    {
        //AI是没有鼠标点击的
        if (attacker.sect != ModelSect.Player)
        {
            return GetTarget_2(skill, attacker);
        }
        List<ModelBase> results = new List<ModelBase>();
        Collider2D col = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);
        if (col != null)
        {
            ModelBase target = col.GetComponent<ModelBase>();
            if (target != null)
            {
                //技能的目标类型 和 技能执行的目标类型配置表一致
                //简单解释 就是不能攻击友方的就不能打友方
                if (target.sect != attacker.sect)
                {
                    results.Add(target);
                }
            }
        }
        return results;
    }

    public static List<ModelBase> GetTarget_1(ISkill skill, ModelBase attacker)
    {
        List<ModelBase> results = new List<ModelBase>();
        for (int i = 0; i < GameApp.FightManager.heros.Count; i++)
        {
            //找到在技能范围内的目标
            if (skill.IsModelInSkillArea(GameApp.FightManager.heros[i]))
            {
                results.Add(GameApp.FightManager.heros[i]);
            }
        }
        for (int i = 0; i < GameApp.FightManager.enemys.Count; i++)
        {
            //找到在技能范围内的目标
            if (skill.IsModelInSkillArea(GameApp.FightManager.enemys[i]))
            {
                results.Add(GameApp.FightManager.enemys[i]);
            }
        }


        return results;
    }

    //2.在攻击范围内的英雄的目标
    public static List<ModelBase> GetTarget_2(ISkill skill, ModelBase attacker)
    {
        List<ModelBase> results = new List<ModelBase>();
        for (int i = 0; i < GameApp.FightManager.heros.Count; i++)
        {
            //找到在技能范围内的目标
            if (skill.IsModelInSkillArea(GameApp.FightManager.heros[i]))
            {
                results.Add(GameApp.FightManager.heros[i]);
            }
        }

        return results;
    }
}
