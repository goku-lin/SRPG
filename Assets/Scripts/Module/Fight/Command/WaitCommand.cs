using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 等待指令
/// </summary>
public class WaitCommand : BaseCommand
{
    private float time;
    System.Action callback;

    public WaitCommand(float t, System.Action callback = null)
    {
        this.time = t;
        this.callback = callback;
    }

    public override bool Update(float dt)
    {
        this.time -= dt;
        if (time <= 0)
        {
            callback?.Invoke();
            return true;
        }
        return false;
    }
}
