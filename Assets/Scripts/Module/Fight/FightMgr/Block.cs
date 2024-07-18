using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Null,   //空
    Obstacle    //障碍物
}

/// <summary>
/// 地图的单元格子
/// </summary>
public class Block : MonoBehaviour
{
    public int RowIndex;    //行下标
    public int ColIndex;    //列下标
    public BlockType Type;  //格子类型
    private SpriteRenderer selectSp;    //选中后图片
    private SpriteRenderer gridSp;      //网格图片
    private SpriteRenderer dirSp;       //移动方向图片

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    private void OnDestroy()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //显示格子
    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }

    //隐藏格子
    public void HideGrid()
    {
        gridSp.enabled = false;
        //gridSp.color = Color.white;
    }

    private void OnSelectCallBack(System.Object arg)
    {
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        if (!GameApp.CommandManager.IsRunningCommand)
        {
            GameApp.ViewManager.Open(ViewType.FightOptionDesView);
        }
    }

    //未选中
    private void OnUnSelectCallBack(System.Object arg)
    {
        dirSp.sprite = null;
        GameApp.ViewManager.Close((int)ViewType.FightOptionDesView);
    }

    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    //设置箭头方向的图片资源 和 颜色
    public void SetDirSp(Sprite sp, Color color)
    {
        dirSp.sprite = sp;
        dirSp.color = color;
    }
}
