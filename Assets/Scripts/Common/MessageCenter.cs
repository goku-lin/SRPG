using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息处理中心
/// </summary>
public class MessageCenter
{
    private Dictionary<string, System.Action<object>> msgDic;   //存储普通的消息字典
    private Dictionary<string, System.Action<object>> tempMsgDic;   //存储临时的消息字典，执行后移除
    private Dictionary<System.Object, Dictionary<string, System.Action<object>>> objMsgDic; //存储特定对象的消息字典

    public MessageCenter() 
    {
        msgDic = new Dictionary<string, System.Action<object>>();
        tempMsgDic = new Dictionary<string, System.Action<object>>();
        objMsgDic = new Dictionary<object, Dictionary<string, System.Action<object>>>();
    }

    //添加事件
    public void AddEvent(string eventName, System.Action<object> callBack)
    {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName] += callBack;
        }
        else
        {
            msgDic.Add(eventName, callBack);
        }
    }

    //移除事件
    public void RemoveEvent(string eventName, System.Action<object> callBack)
    {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName] -= callBack;
            if (msgDic[eventName] == null)
            {
                msgDic.Remove(eventName);
            }
        }
    }

    //执行事件
    public void PostEvent(string eventName, object arg = null)
    {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName].Invoke(arg);
        }
    }

    public void AddEvent(System.Object listenerObj, string eventName, System.Action<object> callBack)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName))
            {
                objMsgDic[listenerObj][eventName] += callBack;
            }
            else
            {
                objMsgDic[listenerObj].Add(eventName, callBack);
            }
        }
        else
        {
            Dictionary<string, System.Action<object>> _tempDic = new Dictionary<string, System.Action<object>>();
            _tempDic.Add(eventName, callBack);
            objMsgDic.Add(listenerObj, _tempDic);
        }
    }

    //移除对象事件
    public void RemoveEvent(System.Object listenerObj, string eventName, System.Action<object> callBack)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName))
            {
                objMsgDic[listenerObj][eventName] -= callBack;
                if (objMsgDic[listenerObj][eventName] == null)
                {
                    objMsgDic[listenerObj].Remove(eventName);
                    if (objMsgDic[listenerObj].Count == 0)
                    {
                        objMsgDic.Remove(listenerObj);
                    }
                }
            }
        }
    }

    //移除对象的所有事件
    public void RemoveObjAllEvent(System.Object listenerObj)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            objMsgDic.Remove(listenerObj);
        }
    }

    //执行对象的事件
    public void PostEvent(System.Object listenerObj, string eventName, System.Object arg = null)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName))
            {
                objMsgDic[listenerObj][eventName].Invoke(arg);
            }
        }
    }

    //添加临时事件 要覆盖 不是累加
    public void AddTempEvent(string eventName, System.Action<object> callBack)
    {
        if (tempMsgDic.ContainsKey(eventName))
        {
            //添加临时事件，然后被覆盖掉
            tempMsgDic[eventName] = callBack;
        }
        else
        {
            tempMsgDic.Add(eventName, callBack);
        }
    }

    public void PostTempEvent(string eventName, System.Object arg = null)
    {
        if (tempMsgDic.ContainsKey(eventName))
        {
            tempMsgDic[eventName].Invoke(arg);
            //Debug.Log($"执行临时事件:{eventName}");
            tempMsgDic[eventName] = null;
            tempMsgDic.Remove(eventName);
        }
    }
}
