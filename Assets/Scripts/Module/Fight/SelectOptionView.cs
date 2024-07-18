using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectOptionView : BaseView
{
    Dictionary<int, OptionItem> opItems;    //存储选项

    public override void InitData()
    {
        base.InitData();
        opItems = new Dictionary<int, OptionItem>();
        FightModel fightModel = Controller.GetModel() as FightModel;

        List<OptionData> ops = fightModel.options;

        Transform tf = Find("bg/grid").transform;
        GameObject prefabObj = Find("bg/grid/item");

        for (int i = 0; i < ops.Count; i++)
        {
            GameObject obj = Object.Instantiate(prefabObj, tf);
            obj.SetActive(false);
            OptionItem item = obj.AddComponent<OptionItem>();
            item.Init(ops[i]);
            opItems.Add(ops[i].Id, item);
        }
    }

    public override void Open(params object[] args)
    {
        //传入两个参数 一个是英雄的Event字符串 对应 选项id 需要切割
        //第二个 角色位置
        //Event 1001-1002-1005
        string[] evtArr = args[0].ToString().Split("-");
        Find("bg/grid").transform.position = (Vector2)args[1];
        foreach (var item in opItems)
        {
            item.Value.gameObject.SetActive(false);
        }

        for (int i = 0; i < evtArr.Length; i++)
        {
            opItems[int.Parse(evtArr[i])].gameObject.SetActive(true);
        }
    }
}
