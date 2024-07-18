using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    private GameTimer timer;    //计时器

    //skill:使用的技能   targets:技能的作用目标 callback：回调
    private Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)> skills;   //技能队列

    public SkillManager() 
    {
        timer = new GameTimer();
        skills = new Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)>();
    }

    //添加技能
    public void AddSkill(ISkill skill, List<ModelBase> targets = null, System.Action callback = null)
    {
        skills.Enqueue((skill, targets, callback));
    }

    //使用技能
    public void UseSkill(ISkill skill, List<ModelBase> targets, System.Action callback)
    {
        ModelBase current = (ModelBase)skill;
        //看向一个目标
        if (targets.Count > 0)
        {
            current.LookAtModel(targets[0]);
        }
        current.PlaySound(skill.skillPro.Sound);    //音效
        current.PlayAni(skill.skillPro.AniName);    //动画
        //延迟攻击
        timer.Register(skill.skillPro.AttackTime, () =>
        {
            //技能的最多作用个数
            int atkCount = skill.skillPro.AttackCount >= targets.Count ? targets.Count : skill.skillPro.AttackCount;

            for (int i = 0; i < atkCount; i++)
            {
                targets[i].GetHit(skill);   //受伤
            }
        });
        //技能的持续时长
        timer.Register(skill.skillPro.Time, () =>
        {
            //回到待机
            current.PlayAni("idle");
            callback?.Invoke();
        });
    }

    public void Update(float dt)
    {
        timer.OnUpdate(dt);
        if (timer.Count() == 0 && skills.Count > 0)
        {
            //下一个使用的技能
            var next = skills.Dequeue();
            if (next.targets != null)
            {
                UseSkill(next.skill, next.targets, next.callback);  //使用技能
            }
        }
    }

    //使用正在跑一个技能
    public bool IsRunningSkill()
    {
        if (timer.Count() == 0 && skills.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //清空技能
    public void Clear()
    {
        timer.Break();
        skills.Clear();
    }
}
