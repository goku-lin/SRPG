using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移动命令
/// </summary>
public class MoveCommand : BaseCommand
{
    private List<AStarPoint> paths;

    private AStarPoint current;
    private int pathIndex;

    //移动前的坐标
    private int preRowIndex;
    private int preColIndex;

    public MoveCommand(ModelBase model) : base(model)
    {

    }

    public MoveCommand(ModelBase model, List<AStarPoint> paths) : base(model)
    {
        this.paths = paths;
        pathIndex = 0;
    }

    public override void Do()
    {
        base.Do();
        this.preColIndex = this.model.ColIndex;
        this.preRowIndex = this.model.RowIndex;
        //设置当前所占的格子为null
        GameApp.MapManager.ChangeBlockType(preRowIndex, preColIndex, BlockType.Null);
    }

    public override bool Update(float dt)
    {
        current = this.paths[pathIndex];
        if (this.model.Move(current.RowIndex, current.ColIndex, dt * 5))
        {
            pathIndex++;
            if (pathIndex > this.paths.Count - 1)
            {
                //到达
                this.model.PlayAni("idle");

                GameApp.MapManager.ChangeBlockType(model.RowIndex, model.ColIndex, BlockType.Obstacle);

                //显示选项界面
                GameApp.ViewManager.Open(ViewType.SelectOptionView, model.data["Event"], (Vector2)model.transform.position);

                return true;
            }
        }

        this.model.PlayAni("move");

        return false;
    }

    //撤销
    public override void Undo()
    {
        base.Undo();

        //回到之前位置
        Vector3 pos = GameApp.MapManager.GetBlockPos(preRowIndex, preColIndex);
        pos.z = this.model.transform.position.z;
        this.model.transform.position = pos;
        GameApp.MapManager.ChangeBlockType(model.RowIndex, model.ColIndex, BlockType.Null);
        this.model.RowIndex = preRowIndex;
        this.model.ColIndex = preColIndex;
        GameApp.MapManager.ChangeBlockType(model.RowIndex, model.ColIndex, BlockType.Obstacle);
    }
}
