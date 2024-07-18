using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制器基类
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message;    //事件字典

    protected BaseModel model;  //模板数据

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }

    //注册后调用的初始化（所有控制器初始化后执行）
    public virtual void Init()
    {

    }

    //加载视图
    public virtual void OnLoadView(IBaseView view) { }

    //打开视图
    public virtual void OpenView(IBaseView view) { }

    //关闭视图
    public virtual void CloseView(IBaseView view) { }

    //注册模板事件
    public void RegisterFunc(string eventName, System.Action<object[]> callBack)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName] += callBack;
        }
        else
        {
            message.Add(eventName, callBack);
        }
    }

    //注销模板事件
    public void UnregisterFunc(string eventName)
    {
        if(message.ContainsKey(eventName))
        {
            message.Remove(eventName);
        }
    }

    /// <summary>
    /// 触发本模块事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public void ApplyFunc(string eventName, params object[] args)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName].Invoke(args);
        }
        else
        {
            Debug.Log("error:" +  eventName);
        }
    }

    /// <summary>
    /// 触发其他控制器的事件
    /// </summary>
    /// <param name="controllerKey"></param>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
    {
        GameApp.ControllerManager.ApplyFunc(controllerKey, eventName, args);
    }

    public void ApplyControllerFunc(ControllerType type, string eventName, params object[] args)
    {
        ApplyControllerFunc((int)type, eventName, args);
    }

    //设置模型数据
    public void SetModel(BaseModel model)
    {
        this.model = model;
        this.model.controller = this;
    }

    public BaseModel GetModel()
    {
        return this.model;
    }

    public T GetModel<T>() where T : BaseModel
    {
        return model as T;
    }

    public BaseModel GetControllerModel(int controllerKey)
    {
        return GameApp.ControllerManager.GetControllerModel(controllerKey);
    }

    /// <summary>
    /// 销毁控制器
    /// </summary>
    public virtual void Destroy()
    {
        RemoveModuleEvent();
        RemoveGlobalEvent();
    }

    //初始化模板事件
    public virtual void InitModuleEvent()
    {

    }

    //移除模板事件
    public virtual void RemoveModuleEvent()
    {

    }

    //初始化全局事件
    public virtual void InitGlobalEvent()
    {

    }

    //移除全局事件
    public virtual void RemoveGlobalEvent()
    {

    }
}
