using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 广度优先搜索算法
/// </summary>
public class _BFS
{
    //搜索点
    public class Point
    {
        public int RowIndex;    //行
        public int ColIndex;    //列
        public Point Father;    //父节点 查找路径用

        public Point(int row, int col)
        {
            this.RowIndex = row;
            this.ColIndex = col;
        }

        public Point(int rowIndex, int colIndex, Point father)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
            Father = father;
        }
    }

    public int RowCount;    //行总数
    public int ColCount;    //列总数

    public Dictionary<string, Point> finds; //存储查找到的点的字典（key：点的行列字符串，value：点

    public _BFS(int rowCount, int colCount)
    {
        finds = new Dictionary<string, Point>();
        this.RowCount = rowCount;
        this.ColCount = colCount;
    }

    /// <summary>
    /// 搜索行走区域
    /// </summary>
    /// <param name="row">开始点的行坐标</param>
    /// <param name="col">开始点的列坐标</param>
    /// <param name="step">步数</param>
    /// <returns></returns>
    public List<Point> Search(int row, int col, int step)
    {
        //定义搜索集合
        List<Point> searchs = new List<Point>();
        //开始点
        Point startPoint = new Point(row, col);
        //存储开始点
        searchs.Add(startPoint);
        //开始点默认已经找到，存储到找到的字典
        finds.Add($"{row}_{col}", startPoint);

        //遍历步数
        for (int i = 0; i < step; i++)
        {
            //定义临时集合，存储目前找到满足条件的点
            List<Point> temps = new List<Point>();
            //遍历搜索集合
            for (int j = 0; j < searchs.Count; j++)
            {
                Point current = searchs[j];
                //查找当前点四周的点
                FindAroundPoints(current, temps);
            }
            if (temps.Count == 0)
            {
                //临时集合一个点都没有，死路了，没必要继续找了、
                break;
            }
            //搜索的集合要清空
            searchs.Clear();
            //临时集合的点添加到搜索集合
            searchs.AddRange(temps);
        }

        //将查找到的点转换成集合 返回
        return finds.Values.ToList();
    }

    //找周围上下左右（可以扩展查找斜方向的点
    public void FindAroundPoints(Point current, List<Point> temps)
    {
        //上
        if (current.RowIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex - 1, current.ColIndex, current, temps);
        }
        //下
        if (current.RowIndex + 1 < RowCount)
        {
            AddFinds(current.RowIndex + 1, current.ColIndex, current, temps);
        }
        //左
        if (current.ColIndex - 1 >= 0)
        {
            AddFinds(current.RowIndex, current.ColIndex - 1, current, temps);
        }
        //右
        if (current.ColIndex + 1 < ColCount)
        {
            AddFinds(current.RowIndex, current.ColIndex + 1, current, temps);
        }
    }

    //添加到查找字典
    public void AddFinds(int row, int col, Point father, List<Point> temps)
    {
        //不在查找节点 且 对应地图格子不是障碍物 才能加入
        if (!finds.ContainsKey($"{row}_{col}") && GameApp.MapManager.GetBlockType(row, col) != BlockType.Obstacle)
        {
            Point p = new Point(row, col, father);
            //添加到查找到的字典
            finds.Add($"{row}_{col}", p);
            //添加到临时集合，用于下次继续寻找
            temps.Add(p);
        }
    }

    //寻路 离目标最近的路径
    public List<Point> FindMinPath(ModelBase model, int step, int endRowIndex, int endColIndex)
    {
        List<Point> results = Search(model.RowIndex, model.ColIndex, step); //能移动的集合
        if (results.Count == 0)
        {
            return null;
        }
        else
        {
            Point minPoint = results[0];    //默认一个点为最近的
            int mid_dis = ManhattanDistance(minPoint, endRowIndex, endColIndex);
            for (int i = 1; i < results.Count; i++)
            {
                int temp_dis = ManhattanDistance(results[i], endRowIndex, endColIndex);
                if (temp_dis < mid_dis)
                {
                    mid_dis = temp_dis;
                    minPoint = results[i];
                }
            }
            List<Point> paths = new List<Point>();
            Point current = minPoint.Father;
            paths.Add(minPoint);
            while (current != null)
            {
                paths.Add(current);
                current = current.Father;
            }
            paths.Reverse();
            return paths;
        }
    }

    private static int ManhattanDistance(Point A, int BRowIndex, int BColIndex)
    {
        return Mathf.Abs(A.RowIndex - BRowIndex) + Mathf.Abs(A.ColIndex - BColIndex);
    }
}