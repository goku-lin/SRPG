using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能指令
/// </summary>
public class SkillCommand : BaseCommand
{
    ISkill skill;

    public SkillCommand(ModelBase model) : base(model)
    {
        skill = model as ISkill;
    }

    public override void Do()
    {
        base.Do();
        List<ModelBase> results = skill.GetTarget(model);
        if (results.Count > 0)
        {
            //有目标
            GameApp.SkillManager.AddSkill(skill, results);  //添加到技能管理器
        }
    }

    public override bool Update(float dt)
    {
        if (!GameApp.SkillManager.IsRunningSkill())
        {
            model.IsStop = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
