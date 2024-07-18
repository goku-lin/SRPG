using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 用户控制管理器 键盘鼠标等
/// </summary>
public class UserInputManager
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //点击到UI
            }
            else
            {
                Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, (Collider2D col) =>
                {
                    if (col != null)
                    {
                        //检测到有碰撞的物体
                        GameApp.MessageCenter.PostEvent(col.gameObject, Defines.OnSelectEvent);
                    }
                    else
                    {
                        //执行未选中
                        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
                    }
                });
            }
        }
    }
}
