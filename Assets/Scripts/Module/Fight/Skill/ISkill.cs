using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISkill
{
    SkillProperty skillPro {  get; set; }

    void ShowSkillArea();

    void HideSkillArea();
}
