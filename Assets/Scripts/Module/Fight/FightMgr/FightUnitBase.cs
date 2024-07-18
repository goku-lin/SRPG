using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗单元
/// </summary>
public class FightUnitBase
{
    public virtual void Init()
    {

    }

    public virtual bool Update(float dt)
    {
        return false;
    }
}
