using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//显示移动路径的指令
public class ShowPathCommand : BaseCommand
{
    Collider2D pre; //鼠标之前检测的2d碰撞盒
    Collider2D current; //鼠标当前检测的2d碰撞盒
    AStar astar;    //A星对象
    AStarPoint start;   //开始点
    AStarPoint end;     //结束点
    List<AStarPoint> prePaths;  //之前检测到的路径集合 用来清空用

    public ShowPathCommand(ModelBase model) : base(model) 
    {
        prePaths = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex, model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount, GameApp.MapManager.ColCount);
    }

    public override bool Update(float dt)
    {
        //点击鼠标后，确定移动位置
        if (Input.GetMouseButtonDown(0))
        {
            if (prePaths.Count != 0 && this.model.Step >= prePaths.Count - 1)
            {
                GameApp.CommandManager.AddCommand(new MoveCommand(this.model, prePaths));   //移动
            }
            else
            {
                GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

                //不移动直接显示操作选项
                //显示选项界面
                GameApp.ViewManager.Open(ViewType.SelectOptionView, model.data["Event"], (Vector2)model.transform.position);
            }
            
            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);   //检测当前鼠标位置是否有2d碰撞体

        if (current != null )
        {
            //之前的检测碰撞和当前的不一致 才进行 路径检测
            if (current != pre)
            {
                pre = current;

                Block b = current.GetComponent<Block>();

                if (b != null)
                {
                    //检测到block的物体，进行寻路
                    end = new AStarPoint(b.RowIndex, b.ColIndex);
                    astar.FindPath(start, end, updatePath);
                }
                else
                {
                    //没检测到，将之前的路径清楚
                    for (int i = 0; i < prePaths.Count; i++)
                    {
                        GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
                    }
                    prePaths.Clear();
                }
            }
        }

        return false;
    }

    private void updatePath(List<AStarPoint> paths)
    {
        //如果之前已经有了路径 要先清楚
        if (prePaths.Count != 0)
        {
            for (int i = 0; i < prePaths.Count; i++)
            {
                GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null, Color.white);
            }
        }

        if (paths.Count >= 2 && model.Step >= paths.Count - 1)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                BlockDirection dir = BlockDirection.down;

                if (i == 0)
                {
                    dir = GameApp.MapManager.GetDirection1(paths[i], paths[i + 1]);
                }
                else if (i == paths.Count - 1)
                {
                    dir = GameApp.MapManager.GetDirection2(paths[i], paths[i - 1]);
                }
                else
                {
                    dir = GameApp.MapManager.GetDirection3(paths[i-1], paths[i], paths[i+1]);
                }

                GameApp.MapManager.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir, Color.yellow);
            }
        }
        //这个不能放在上面的循环里面，因为这样会只保留最后在范围的一项
        prePaths = paths;
    }

}
