using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 拖拽出来的图标界面
/// </summary>
public class DragHeroView : BaseView
{
    private void Update()
    {
        //拖拽中跟随鼠标移动，显示的时候才会移动
        if (!_canvas.enabled)
        {
            return;
        }

        //鼠标坐标转换世界坐标
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPos;
    }

    public override void Open(params object[] args)
    {
        transform.GetComponent<Image>().SetIcon(args[0].ToString());
    }
}
