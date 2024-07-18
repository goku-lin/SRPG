using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 处理拖拽英雄图标的脚本
/// </summary>
public class HeroItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Dictionary<string, string> data;

    private void Start()
    {
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Close((int)ViewType.DragHeroView);
        //检测拖拽后的位置是否后block脚本
        Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, (Collider2D col) =>
        {
            if (col != null)
            {
                Block b = col.GetComponent<Block>();
                if (b != null)
                {
                    //有方块
                    Debug.Log(b);
                    //删除拖拽物体并生成英雄
                    Destroy(gameObject);
                    GameApp.FightManager.AddHero(b, data, ModelSect.Player);
                }
            }
        });
        
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
