using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模型基类
/// </summary>
public class BaseModel
{
    public BaseController controller;

    public BaseModel(BaseController ctl)
    {
        this.controller = ctl;
    }

    public BaseModel()
    {

    }

    public virtual void Init()
    {

    }
}
