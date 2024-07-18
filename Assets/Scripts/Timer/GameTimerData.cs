using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerData
{
    private float timer;    //倒计时
    private System.Action callBack; //回调
    public GameTimerData(float timer, System.Action callback)
    {
        this.timer = timer;
        this.callBack = callback;
    }

    public bool OnUpdate(float dt)
    {
        timer -= dt;
        if (timer <= 0)
        {
            this.callBack.Invoke();
            return true;
        }
        return false;
    }
}
